using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDatas();
}
