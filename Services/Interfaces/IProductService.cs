using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface IProductService
{
    Task<(List<ProductRespViewModel>, int)> FindAll(int pageNumber, int pageSize);
    ProductRespViewModel FindById(Guid id);
    List<ProductRespViewModel> FindAllByDescription(string description);
    ProductRespViewModel Created(ProductPostViewModel productViewModel);
    void Updated(ProductPutViewModel productViewModel);
    void Delete(Guid id);
}
