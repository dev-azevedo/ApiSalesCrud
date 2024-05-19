using Microsoft.EntityFrameworkCore;
using SalesCrud.Infra;
using SalesCrud.Model;
using SalesCrud.Repository.Interfaces;

namespace SalesCrud.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly AppDbContext _context;
    protected DbSet<T> dataset;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        dataset = _context.Set<T>();
    }

    public virtual async Task<(List<T>, int)> FindAll(int pageNumber, int pageSize)
    {
        var totalItems = await dataset.CountAsync();
        var products =  await dataset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return (products, totalItems);

    }
    
    public virtual T FindById(Guid id)
    {
        return dataset.FirstOrDefault(x => x.Id.Equals(id));
    }

    public virtual T Created(T item)
    {
        try
        {
            dataset.Add(item);
            _context.SaveChanges();
            return item;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual T Update(T item)
    {
        var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));

        if (result == null) return null;

        try
        {
            _context.Entry(result).CurrentValues.SetValues(item);
            _context.SaveChanges();
            return result;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public virtual void Delete(Guid id)
    {
        var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
        if (result != null)
        {
            try
            {
                dataset.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public virtual bool Exists(Guid id)
    {
        return dataset.Any(p => p.Id.Equals(id));
    }
}
