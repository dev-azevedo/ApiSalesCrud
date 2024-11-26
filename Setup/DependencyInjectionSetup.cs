using SalesCrud.Config;
using SalesCrud.Repository;
using SalesCrud.Repository.Interfaces;
using SalesCrud.Services;
using SalesCrud.Services.Interfaces;
namespace SalesCrud.Setup;

public static class DependencyInjectionSetup
{
    public static void AddDIConfig(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IHostedService, RoleInitializer>();
        services.AddAutoMapper(typeof(AutoMapperConfig));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDashboardService, DashBoardService>();
    }
}
