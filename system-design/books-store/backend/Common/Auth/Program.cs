using Auth.WebApi.Application.Models;
using Auth.WebApi.Persistence;
using Common.Extensions.API.Observability;
using Common.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.WebApi;

public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var apiServicesConfiguration = ApiServicesConfiguration.Default with
        {
            AddAuthentication = false,
            AddAuthorization = false,
            AddFastEndpoints = false
        };

        builder.ConfigureObservability();

        builder.Services.RegisterApiServices(apiServicesConfiguration);

        builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseNpgsql(builder.Configuration.GetConnectionString("AuthDatabaseConnectionString"));
            })
            .AddIdentityCore<User>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddApiEndpoints();

        var app = builder.Build();

        app.ConfigureApi(apiServicesConfiguration);

        app.ApplyMigrations<UserDbContext>();

        app.MapIdentityApi<User>();

        await app.RunAsync();
    }
}