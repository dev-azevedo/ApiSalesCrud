using SalesCrud.Model;

namespace SalesCrud.Repository.Interfaces;

public interface ISaleRepository : IGenericRepository<Sale>
{
    Task<int> Count();
    List<Sale> FindAllByNameOrDescription(string nameOrDescription);

    Task<int> ProductBestSeller();
}
