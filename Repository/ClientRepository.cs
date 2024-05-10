using SalesCrud.Infra;
using SalesCrud.Model;
using SalesCrud.Repository.Interfaces;
using System.Data;

namespace SalesCrud.Repository;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{

    public ClientRepository(AppDbContext context) : base(context)
    { }

    public Client FindByName(string name)
    {
        return dataset.SingleOrDefault(p => p.Name == name);
    }

    public List<Client> FindAllByName(string name)
    {
        return dataset.Where(p => p.Name.Contains(name)).ToList();
    }
}
