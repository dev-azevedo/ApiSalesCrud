using SalesCrud.Model;

namespace SalesCrud.Repository.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Product FindByDescription(string description);
    List<Product> FindAllByDescription(string description);
}
