using SalesCrud.ViewModel;
using SalesCrud.Exceptions;

namespace SalesCrud.Services.Interfaces;
public interface IAuthService
{
    Task<ValidationResultModel> Register(AuthRegisterViewModel model);
}
