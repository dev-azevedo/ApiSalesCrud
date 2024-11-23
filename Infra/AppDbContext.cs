using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesCrud.Model;

namespace SalesCrud.Infra;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{

    public DbSet<Sale> Sales { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }

    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=SalesCrud.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity
                .Property(e => e.Id)
                .HasColumnType("TEXT")
                .HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity.Property(e => e.Name).HasColumnType("TEXT");
            entity.Property(e => e.Email).HasColumnType("TEXT");
            entity.Property(e => e.City).HasColumnType("TEXT");
            entity.Property(e => e.PathImage).HasColumnType("TEXT").IsRequired(false);
            entity
                .Property(e => e.CreatedOn)
                .HasColumnType("TEXT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.EditedOn).HasColumnType("TEXT");

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasMany(c => c.Sales).WithOne(s => s.Client).HasForeignKey(c => c.Id);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .Property(e => e.Id)
                .HasColumnType("TEXT")
                .HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity.Property(e => e.Description).HasColumnType("TEXT");
            entity.Property(e => e.UnitaryValue).HasColumnType("TEXT");
            entity.Property(e => e.PathImage).HasColumnType("TEXT").IsRequired(false);
            entity
                .Property(e => e.CreatedOn)
                .HasColumnType("TEXT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.EditedOn).HasColumnType("TEXT").IsRequired(false);

            entity.HasMany(p => p.Sales).WithOne(s => s.Product).HasForeignKey(p => p.Id);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity
                .Property(e => e.Id)
                .HasColumnType("TEXT")
                .HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity
                .Property(e => e.ProductId)
                .HasColumnType("TEXT")
                .HasConversion(v => v.ToString(), v => Guid.Parse(v));
            entity
                .Property(e => e.ClientId)
                .HasColumnType("TEXT")
                .HasConversion(v => v.ToString(), v => Guid.Parse(v));

            entity.Property(e => e.ValueSale).HasColumnType("TEXT");
            entity.Property(e => e.ProductQuantity).HasColumnType("INTEGER");
            entity
                .Property(e => e.CreatedOn)
                .HasColumnType("TEXT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.EditedOn).HasColumnType("TEXT").IsRequired(false);

            entity.HasOne(s => s.Client).WithMany(c => c.Sales).HasForeignKey(s => s.ClientId);
            entity.HasOne(s => s.Product).WithMany(p => p.Sales).HasForeignKey(s => s.ProductId);
            entity.HasOne(s => s.User).WithMany().HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict); ;
        });
    }
}
