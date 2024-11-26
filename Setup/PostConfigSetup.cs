using Microsoft.AspNetCore.Mvc;
using SalesCrud.Exceptions;

namespace SalesCrud.Setup;
public static class PostConfigSetup
{
    public static void AddPostConfig(this IServiceCollection services)
    {
        services.PostConfigure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext
                    .ModelState.Keys.SelectMany(key =>
                        actionContext
                            .ModelState[key]
                            .Errors.Select(x => new ValidationError(x.ErrorMessage))
                    )
                    .ToList();

                var model = new ValidationResultModel(400, errors);
                return new BadRequestObjectResult(model);
            };
        });
    }
}
