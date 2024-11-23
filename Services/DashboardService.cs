using ApiSalesCrud.Services.Interfaces;
using ApiSalesCrud.ViewModel;
using SalesCrud.Repository.Interfaces;

namespace ApiSalesCrud.Services;

public class DashBoardService : IDashboardService
{
    private readonly IClientRepository _clientRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;

    public DashBoardService(IClientRepository clientRepository, IProductRepository productRepository, ISaleRepository saleRepository)
    {
        _clientRepository = clientRepository;
        _productRepository = productRepository;
        _saleRepository = saleRepository;
    }
    public async Task<DashboardViewModel> GetDatas()
    {
       var TotalClients = await _clientRepository.Count();
       var TotalProducts = await _productRepository.Count();
       var TotalSales = await _saleRepository.Count();
       var ProductBestSellerCount = await _saleRepository.ProductBestSeller();

       return new DashboardViewModel() {
           TotalClients = TotalClients,
           TotalProducts = TotalProducts,
           TotalSales = TotalSales,
           ProductBestSeller = ProductBestSellerCount
       };
    }
}
