using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface ISaleService
{
    Task<(List<SaleRespViewModel>, int)> FindAll(int pageNumber, int pageSize);
    SaleRespViewModel FindById(Guid id);
    List<SaleRespViewModel> FindAllByNameOrDescription(string nameOrDescription);
    SaleRespViewModel Created(SalePostViewModel saleViewModel);
    void Updated(SalePutViewModel saleViewModel);
    void Delete(Guid id);
}
