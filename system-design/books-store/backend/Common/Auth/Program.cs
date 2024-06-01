using Auth.WebApi.Application.Models;
using Auth.WebApi.Persistence;
using Common.Extensions.API.Observability;
using Common.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

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
            AddFastEndpoints = false,
            SwaggerGenOptions = (opt) =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth Service", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            }
        };

        builder.ConfigureObservability();

        builder.Services.RegisterApiServices(apiServicesConfiguration);

        builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
            .AddBearerToken(IdentityConstants.BearerScheme)
            //.AddIdentityCookies()
            ;



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

        app.MapGroup("identity").MapIdentityApi<User>();

        app.MapGet("identity/hello-user", (ClaimsPrincipal user) =>
        {
            return $"Hello, {user.Identity!.Name}";
        })
            .RequireAuthorization();

        await app.RunAsync();
    }
}