using CamposDealerCrud.Model;
using CamposDealerCrud.ViewModel;

namespace CamposDealerCrud.Services.Interfaces;

public interface IProductService
{
    List<ProductRespViewModel> FindAll();
    ProductRespViewModel FindById(Guid id);
    List<ProductRespViewModel> FindAllByDescription(string description);
    ProductRespViewModel Created(ProductPostViewModel productViewModel);
    void Updated(ProductPutViewModel productViewModel);
    void Delete(Guid id);
}
