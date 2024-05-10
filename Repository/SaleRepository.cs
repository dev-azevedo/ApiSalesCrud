using SalesCrud.Infra;
using SalesCrud.Model;
using SalesCrud.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace SalesCrud.Repository;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Sale> FindAll()
    {
        return _context.Sales
                   .Include(s => s.Product)
                   .Include(s => s.Client)
                   .AsNoTracking()
                   .ToList();
    }

    public Sale FindById(Guid id)
    {
        return _context.Sales
                   .Include(s => s.Product)
                   .Include(s => s.Client)
                   .AsNoTracking()
                   .SingleOrDefault(s => s.Id.Equals(id));
    }

    public List<Sale> FindAllByNameOrDescription(string nameOrDescription)
    {
        return _context.Sales
            .Include(s => s.Product)
            .Include(s => s.Client)
            .AsNoTracking()
            .Where(s => s.Client.Name.Contains(nameOrDescription) || s.Product.Description.Contains(nameOrDescription))
            .ToList();
    }

    public void Created(Sale sale)
    {
        _context.Sales.Add(sale);
        _context.SaveChanges();
    }

    public void Update(Sale sale)
    {
        _context.Sales.Update(sale);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var saleDb = _context.Sales.SingleOrDefault(sale => sale.Id.Equals(id));
        if (saleDb != null)
        {
            _context.Sales.Remove(saleDb);
            _context.SaveChanges();
        }
    }

    public bool Exists(Guid id)
    {
        return _context.Sales.Any(p => p.Id.Equals(id));
    }
}
