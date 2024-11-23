using ApiSalesCrud.ViewModel;

namespace ApiSalesCrud.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDatas();
}
