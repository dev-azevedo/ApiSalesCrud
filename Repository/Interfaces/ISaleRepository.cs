using SalesCrud.Model;

namespace SalesCrud.Repository.Interfaces;

public interface ISaleRepository : IGenericRepository<Sale>
{
    List<Sale> FindAllByNameOrDescription(string nameOrDescription);
}
