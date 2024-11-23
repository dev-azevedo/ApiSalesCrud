using SalesCrud.Model;

namespace SalesCrud.Repository.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<int> Count();
    Product FindByDescription(string description);
    List<Product> FindAllByDescription(string description);
}
