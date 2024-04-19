using CamposDealerCrud.Model;

namespace CamposDealerCrud.Repository.Interfaces;

public interface IClientRepository
{
    List<Client> FindAll();
    Client FindById(Guid id);
    Client FindByName(string name);
    List<Client> FindAllByName(string name);
    void Created(Client client);
    void Update(Client client);
    void Delete(Guid id);
    bool Exists(Guid id);
}
