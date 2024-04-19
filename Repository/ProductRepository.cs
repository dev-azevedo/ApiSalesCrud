using CamposDealerCrud.Infra;
using CamposDealerCrud.Model;
using CamposDealerCrud.Repository.Interfaces;
using System.Xml.Linq;

namespace CamposDealerCrud.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Product> FindAll()
    {
        return _context.Products.ToList();

    }

    public Product FindById(Guid id)
    {
        return _context.Products.SingleOrDefault(p => p.Id.Equals(id));

    }
    public Product FindByDescription(string description)
    {
        return _context.Products.SingleOrDefault(p => p.Description == description);
    }

    public List<Product> FindAllByDescription(string description)
    {
        return _context.Products.Where(p => p.Description.Contains(description)).ToList();
    }

    public void Created(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var productDb = _context.Products.SingleOrDefault(p => p.Id.Equals(id));
        if (productDb != null)
        {
            _context.Products.Remove(productDb);
            _context.SaveChanges();

        }
    }

    public bool Exists(Guid id)
    {
        return _context.Products.Any(p => p.Id.Equals(id));
    }

  
}
