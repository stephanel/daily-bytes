using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Common.Infrastructure.Persistence;
using Books.Domain.Books;

namespace Books.Infrastructure.Persistence;

internal class BooksDbContext : DbContextBase
{
    protected override string ApplicationName => "Books.API";
    protected override string DatabaseConnectionString => "BooksDatabaseConnectionString";

    //public DbSet<Book> Books => Set<Book>();

    public BooksDbContext(IConfiguration configuration) : base(configuration)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(BooksDbContext))!);
    }
}
