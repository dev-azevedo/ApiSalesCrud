using ApiSalesCrud.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesCrud.Exceptions;
using System.Security.Claims;

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
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, model.DateOfBirth.ToString()));

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
