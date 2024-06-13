using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalesCrud.Exceptions;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;

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
        IAuthService authService
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Register([FromBody] AuthSignUprViewModel model)
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

    [HttpPost("signin")]
    public async Task<IActionResult> Login([FromBody] AuthSignInViewModel model)
    {
         if (!ModelState.IsValid)
        {
            var errors = new List<ValidationError>();
            foreach (var modelState in ModelState.Values)
                foreach (var error in modelState.Errors)
                    errors.Add(new ValidationError(error.ErrorMessage));

            return BadRequest(new ValidationResultModel(400, errors));
        }


        var user = await _authService.SignIn(model);

        if (user == null)
            return Unauthorized(new ValidationResultModel(401, [new("Usuário não autenticado")]));

        return Ok(user);
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

  
}
