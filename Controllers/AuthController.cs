using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SalesCrud.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalesCrud.Exceptions;
using SalesCrud.Services.Interfaces;

namespace SalesCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration,
        IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = new List<ValidationError>();
            foreach (var modelState in ModelState.Values)
                foreach (var error in modelState.Errors)
                    errors.Add(new ValidationError(error.ErrorMessage));

            return BadRequest(new ValidationResultModel(400, errors));
        }

        var result = await _authService.Register(model);

         if (result.Status == 200)
            return Ok("Usuário criado com sucesso");

        return BadRequest(result);
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

    [HttpPost("validate")]
    public IActionResult ValidateToken([FromBody] string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["TokenConfigurations:SecretKey"]);

        try
        {
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                },
                out SecurityToken validatedToken
            );

            return Ok(new { Valid = true });
        }
        catch
        {
            // Token inválido
            return BadRequest(new ValidationResultModel(400, [new("Token inválido")]));
        }
    }

    private string GenereteToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["TokenConfigurations:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
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
