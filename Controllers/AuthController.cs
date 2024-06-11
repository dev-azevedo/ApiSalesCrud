using ApiSalesCrud.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesCrud.Exceptions;

namespace ApiSalesCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest("As senhas não coincidem");
        }

        try
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
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

        try
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false
            );
            if (result.Succeeded)
            {
                return Ok("Usuário autenticado com sucesso");
            }

            return Unauthorized(
                new ValidationResultModel(
                    401,
                    new List<ValidationError> { new("Usuário não autenticado") }
                )
            );
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };
            return BadRequest(new ValidationResultModel(400, errors));
        }
    }
}
