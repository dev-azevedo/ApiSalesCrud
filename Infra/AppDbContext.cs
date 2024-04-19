using CamposDealerCrud.Model;
using Microsoft.EntityFrameworkCore;

namespace CamposDealerCrud.Infra;

public class AppDbContext : DbContext
{
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração da relação entre Client e Sale
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Client)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.ClientId);

        // Configuração da relação entre Product e Sale
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Product)
            .WithMany(p => p.Sales)
            .HasForeignKey(s => s.ProductId);
    }
}
