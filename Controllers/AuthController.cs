using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiSalesCrud.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalesCrud.Exceptions;

namespace ApiSalesCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = new List<ValidationError>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(new ValidationError(error.ErrorMessage));
                }
            }
            return BadRequest(new ValidationResultModel(400, errors));
        }

        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest(
                new ValidationResultModel(
                    401,
                    new List<ValidationError> { new("As senhas não coincidem") }
                )
            );
        }

        try
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.UserRole.ToString());
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, model.FullName));
                await _userManager.AddClaimAsync(
                    user,
                    new Claim(ClaimTypes.DateOfBirth, model.DateOfBirth.ToString())
                );

                return Ok("Usuário criado com sucesso");
            }

            var errors = new List<ValidationError>();
            foreach (var error in result.Errors)
            {
                errors.Add(new ValidationError(error.Description));
            }

            return BadRequest(new ValidationResultModel(400, errors));
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };
            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return Unauthorized(
                new ValidationResultModel(
                    401,
                    new List<ValidationError> { new("Usuário não autenticado") }
                )
            );
        }

        try
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false
            );

            if (!result.Succeeded)
            {
                return Unauthorized(
                    new ValidationResultModel(
                        401,
                        new List<ValidationError> { new("Usuário não autenticado") }
                    )
                );
            }

            var token = GenereteToken(user);

            var claims = await _userManager.GetClaimsAsync(user);

            var fullNameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var fullName = fullNameClaim?.Value;

            return Ok(
                new
                {
                    token,
                    fullName,
                    Id = user.Id,
                    Email = user.Email
                }
            );
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };
            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    private string GenereteToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                }
            ),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
