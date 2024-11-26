using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalesCrud.Config;
using System.Text;

namespace SalesCrud.Setup;

public static class JwtSetup
{
    public static void AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenConfiguration = new TokenConfiguration();

        new ConfigureFromConfigurationOptions<TokenConfiguration>(
            configuration.GetSection("TokenConfigurations")
        ).Configure(tokenConfiguration);

        services.AddSingleton(tokenConfiguration);


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenConfiguration.Issuer,
                ValidAudience = tokenConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(tokenConfiguration.SecretKey)
                )
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.NoResult();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "text/plain";
                    return context.Response.WriteAsync("Unauthorized");
                }
            };
        });


        services.AddAuthorizationBuilder()
        .AddPolicy(
            "Bearer",
            new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build()
        );
    }
}
