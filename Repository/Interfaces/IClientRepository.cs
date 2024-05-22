using SalesCrud.Model;

namespace SalesCrud.Repository.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Client FindByName(string name);
    List<Client> FindAllByName(string name);
    Task<(Client, int)> FindBestSeller();
}
