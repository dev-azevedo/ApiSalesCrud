using SalesCrud.Model;
using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface IProductService
{
    List<ProductRespViewModel> FindAll();
    ProductRespViewModel FindById(Guid id);
    List<ProductRespViewModel> FindAllByDescription(string description);
    ProductRespViewModel Created(ProductPostViewModel productViewModel);
    void Updated(ProductPutViewModel productViewModel);
    void Delete(Guid id);
}
