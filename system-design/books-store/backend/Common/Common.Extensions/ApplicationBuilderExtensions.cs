using Microsoft.AspNetCore.Builder;

namespace BookStore.Common.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigureApi(this IApplicationBuilder app, bool isDevelopmentEnvironment)
    {
        if(isDevelopmentEnvironment)
        {
            app.UseSwagger().UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        return app;
    }
}
