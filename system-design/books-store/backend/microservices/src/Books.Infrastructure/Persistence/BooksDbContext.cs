using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Reflection;

namespace Books.Infrastructure.Persistence;

internal class BooksDbContext : DbContext
{
    private const string ApplicationName = "Books,API";
    private const string BooksDatabaseConnectionString = "BooksDatabaseConnectionString";

    private readonly string _connectionString = null!;

    public BooksDbContext(IConfiguration configuration)
    {
        var originalConnectionString = configuration.GetConnectionString(BooksDatabaseConnectionString)
            ?? throw new ArgumentNullException(nameof(configuration), "${nameof(BooksDatabaseConnectionString)} is null");

        var builder = new NpgsqlConnectionStringBuilder(originalConnectionString);
        if (!builder.TryGetValue("ApplicationName", out object? value))
        {
            builder.ApplicationName = ApplicationName;
        }

        _connectionString = builder.ToString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connectionString)
            .UseSnakeCaseNamingConvention();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("uuid-ossp")
            .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(BooksDbContext))!);
    }
}
