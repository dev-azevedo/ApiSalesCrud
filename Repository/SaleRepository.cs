using SalesCrud.Infra;
using SalesCrud.Model;
using SalesCrud.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SalesCrud.Repository;

public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{

    public SaleRepository(AppDbContext context) : base(context)
    { }

    public async Task<int> Count()
    {
        return await dataset.CountAsync();
    }

    public override async Task<(List<Sale>, int)> FindAll(int pageNumber, int pageSize)
    {
        var totalItems = await dataset.CountAsync();
        var sales = await dataset
            .OrderByDescending(x => x.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(s => s.Product)
            .Include(s => s.Client)
            .Include(s => s.User)
            .AsNoTracking()
            .ToListAsync();

        return (sales, totalItems);
    }

    public override Sale FindById(Guid id)
    {
        return dataset
                   .Include(s => s.Product)
                   .Include(s => s.Client)
                   .Include(s => s.User)
                   .AsNoTracking()
                   .SingleOrDefault(s => s.Id.Equals(id));
    }

    public List<Sale> FindAllByNameOrDescription(string nameOrDescription)
    {
        var nameOrDescriptionLower = nameOrDescription.ToLower();
        return dataset
            .Include(s => s.Product)
            .Include(s => s.Client)
            .Include(s => s.User)
            .AsNoTracking()
            .Where(s => s.Client.Name.ToLower().Contains(nameOrDescriptionLower) || s.Product.Description.ToLower().Contains(nameOrDescriptionLower))
            .ToList();
    }

    public async Task<int> ProductBestSeller()
    {
        var bestSeller = await dataset
        .GroupBy(s => s.ProductId)
        .Select(group => new
        {
            ProductId = group.Key,
            TotalSales = group.Count()
        })
        .OrderByDescending(g => g.TotalSales)
        .FirstOrDefaultAsync();

        return bestSeller?.TotalSales ?? 0;
    }
}
