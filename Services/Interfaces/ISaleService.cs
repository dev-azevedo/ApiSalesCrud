using SalesCrud.Model;
using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface ISaleService
{
    List<SaleRespViewModel> FindAll();
    SaleRespViewModel FindById(Guid id);
    List<SaleRespViewModel> FindAllByNameOrDescription(string nameOrDescription);
    SaleRespViewModel Created(SalePostViewModel saleViewModel);
    void Updated(SalePutViewModel saleViewModel);
    void Delete(Guid id);
}
