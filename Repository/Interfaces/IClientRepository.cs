using SalesCrud.Model;

namespace SalesCrud.Repository.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Client FindByEmail(string email);
    List<Client> FindAllByName(string name);
    Task<List<(Client, int)>> FindBestSeller();
}
