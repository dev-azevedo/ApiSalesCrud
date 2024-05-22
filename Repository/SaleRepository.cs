using SalesCrud.Infra;
using SalesCrud.Model;
using SalesCrud.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace SalesCrud.Repository;

public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{

    public SaleRepository(AppDbContext context) : base(context)
    { }

    public override async Task<(List<Sale>, int)> FindAll(int pageNumber, int pageSize)
    {
        var totalItems = await dataset.CountAsync();
        var sales = await dataset
            .OrderByDescending(x => x.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(s => s.Product)
            .Include(s => s.Client)
            .AsNoTracking()
            .ToListAsync();

        return (sales, totalItems);
    }

    public override Sale FindById(Guid id)
    {
        return dataset
                   .Include(s => s.Product)
                   .Include(s => s.Client)
                   .AsNoTracking()
                   .SingleOrDefault(s => s.Id.Equals(id));
    }

    public List<Sale> FindAllByNameOrDescription(string nameOrDescription)
    {
        return dataset
            .Include(s => s.Product)
            .Include(s => s.Client)
            .AsNoTracking()
            .Where(s => s.Client.Name.Contains(nameOrDescription) || s.Product.Description.Contains(nameOrDescription))
            .ToList();
    }
}
