using CamposDealerCrud.Infra;
using CamposDealerCrud.Model;
using CamposDealerCrud.Repository.Interfaces;

namespace CamposDealerCrud.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

   

    public List<Client> FindAll()
    {
        return _context.Clients.ToList();
    }

    public Client FindById(Guid id)
    {
        return _context.Clients.SingleOrDefault(c => c.Id.Equals(id));
    }

    public Client FindByName(string name)
    {
        return _context.Clients.SingleOrDefault(p => p.Name == name);
    }

    public List<Client> FindAllByName(string name)
    {
        return _context.Clients.Where(p => p.Name.Contains(name)).ToList();
    }

    public void Created(Client client)
    {

        _context.Clients.Add(client);
        _context.SaveChanges();
    }

    public void Update(Client client)
    {
        _context.Clients.Update(client);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var clientDb = _context.Clients.SingleOrDefault(p => p.Id.Equals(id));
        if (clientDb != null)
        {
            _context.Clients.Remove(clientDb);
            _context.SaveChanges();
        }
    }

    public bool Exists(Guid id)
    {
        return _context.Clients.Any(p => p.Id.Equals(id));
    }
}
