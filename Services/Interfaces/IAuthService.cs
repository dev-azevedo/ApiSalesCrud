using SalesCrud.ViewModel;
using SalesCrud.Exceptions;

namespace SalesCrud.Services.Interfaces;
public interface IAuthService
{
    Task<ValidationResultModel> Register(AuthSignUprViewModel model);
    Task<AuthSignInRespViewModel> SignIn(AuthSignInViewModel model);
    Task<AuthSignInRespViewModel> validateToken(string token);
}
