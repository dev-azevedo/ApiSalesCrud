using Microsoft.AspNetCore.Identity;
using SalesCrud.Infra;

namespace SalesCrud.Setup;

public static class IdentitySetup
{
    public static void AddIdentityConfig(this IServiceCollection services)
    {

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    }

}
