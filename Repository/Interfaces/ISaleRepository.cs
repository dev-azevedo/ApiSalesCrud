using CamposDealerCrud.Model;

namespace CamposDealerCrud.Repository.Interfaces;

public interface ISaleRepository
{
    List<Sale> FindAll();
    Sale FindById(Guid id);
    List<Sale> FindAllByNameOrDescription(string nameOrDescription);
    void Created(Sale sale);
    void Update(Sale sale);
    void Delete(Guid id);
    bool Exists(Guid id);
}
