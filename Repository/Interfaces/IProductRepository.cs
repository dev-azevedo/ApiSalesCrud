using CamposDealerCrud.Model;

namespace CamposDealerCrud.Repository.Interfaces;

public interface IProductRepository
{
    List<Product> FindAll();
    Product FindById(Guid id);
    Product FindByDescription(string description);
    List<Product> FindAllByDescription(string description);
    void Created(Product product);
    void Update(Product product);
    void Delete(Guid id);
    bool Exists(Guid id);
}
