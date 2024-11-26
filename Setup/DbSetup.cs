using Microsoft.EntityFrameworkCore;
using SalesCrud.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesCrud.Setup;

public static class DbSetup
{
    public static void AddDBConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("ConnectionSqLite"))
        );
    }
}
