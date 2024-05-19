using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Reflection;

namespace Common.Infrastructure.Persistence;

public abstract class DbContextBase : DbContext
{
    private readonly IConfiguration _configuration;

    protected abstract string ApplicationName { get; }
    protected abstract string DatabaseConnectionString { get; }
    protected abstract string DbSchema { get; }

    protected DbContextBase(IConfiguration configuration)
    {
        _configuration = configuration;

        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.LazyLoadingEnabled = false;
    }

    private string GetConnectionString()
    {
        var originalConnectionString = _configuration.GetConnectionString(DatabaseConnectionString)
            ?? throw new InvalidOperationException("Database connection string not found");

        var builder = new NpgsqlConnectionStringBuilder(originalConnectionString);
        if (!builder.TryGetValue("ApplicationName", out object? value))
        {
            builder.ApplicationName = ApplicationName;
        }

        return builder.ToString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(GetConnectionString())
            .UseSnakeCaseNamingConvention();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema(DbSchema)
            .HasPostgresExtension("uuid-ossp");
    }
}
