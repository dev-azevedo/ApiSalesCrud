using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SalesCrud.Exceptions;
using SalesCrud.ViewModel;
using SalesCrud.Services.Interfaces;

namespace SalesCrud.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ValidationResultModel> Register(AuthRegisterViewModel model)
    {
        if (model.Password != model.ConfirmPassword)
        {
            return new ValidationResultModel(400, [new("As senhas não coincidem")]);
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

                return new ValidationResultModel(
                    200,
                    new List<ValidationError> { new("Usuário criado com sucesso") }
                );
            }

            var errors = new List<ValidationError>();
            foreach (var error in result.Errors)
            {
                errors.Add(new ValidationError(error.Description));
            }

            return new ValidationResultModel(400, errors);
        }
        catch (Exception ex)
        {
            var errors = new List<ValidationError> { new(ex.Message) };
            return new ValidationResultModel(400, errors);
        }
    }
}
