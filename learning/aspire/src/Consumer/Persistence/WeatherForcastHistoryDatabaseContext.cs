using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Persistence;

public class WeatherForcastHistoryDatabaseContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(WeatherForcastHistoryDatabaseContext))!);
    }
}