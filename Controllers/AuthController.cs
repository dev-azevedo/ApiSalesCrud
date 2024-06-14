using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> SignUp([FromBody] AuthSignUprViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = new List<ValidationError>();
            foreach (var modelState in ModelState.Values)
            foreach (var error in modelState.Errors)
                errors.Add(new ValidationError(error.ErrorMessage));

            return BadRequest(new ValidationResultModel(400, errors));
        }

        try
        {
            var result = await _authService.Register(model);

            if (result.Status == 200)
                return Ok("Usuário criado com sucesso");

            return BadRequest(result);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };
            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] AuthSignInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = new List<ValidationError>();
            foreach (var modelState in ModelState.Values)
            foreach (var error in modelState.Errors)
                errors.Add(new ValidationError(error.ErrorMessage));

            return BadRequest(new ValidationResultModel(400, errors));
        }

        try
        {
            var user = await _authService.SignIn(model);

            if (user == null)
                return BadRequest(
                    new ValidationResultModel(401, [new("Email e/ou senha incorreto.")])
                );

            return Ok(user);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };
            return BadRequest(new ValidationResultModel(400, errors));
        }
    }

    [HttpGet("validate")]
    public async Task<IActionResult> ValidateToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["TokenConfigurations:SecretKey"]);

        try
        {
            var authorizationHeader = Request.Headers.Authorization.ToString();
            if (
                string.IsNullOrEmpty(authorizationHeader)
                || !authorizationHeader.StartsWith("Bearer ")
            )
               return BadRequest(new ValidationResultModel(401, [new("token inválido.")]));
               

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            
            var user = await _authService.validateToken(token);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };
            return BadRequest(new ValidationResultModel(400, errors));
        }
    }
}
