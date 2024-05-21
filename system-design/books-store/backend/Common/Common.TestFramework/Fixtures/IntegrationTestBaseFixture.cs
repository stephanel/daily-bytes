using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Common.TestFramework.Fixtures;

public abstract class IntegrationTestBaseFixture  : IAsyncLifetime, IDisposable
{
    private readonly IMessageSink _messageSink;

    protected IMessageSink MessageSink => _messageSink;

    private readonly PostgreSqlTestDatabaseBuilder _testDatabaseBuilder = new();

    protected string? DatabaseConnectionString { get; private set; }

    protected abstract IServiceProvider? ServiceProvider { get; }

    protected IntegrationTestBaseFixture(IMessageSink messageSink)
    {
        _messageSink = messageSink;
    }

    /// <summary>
    /// This method is ionvoked by XUnit. Do not invoke directly.
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync()
    {
        await _testDatabaseBuilder.Container.StartAsync();
        DatabaseConnectionString = _testDatabaseBuilder.Container.GetConnectionString();
        await CustomInitializeAsync();
    }

    protected abstract Task CustomInitializeAsync();

    protected async Task MigrateAsync<TDbContext>(CancellationToken cancellationToken = default)
        where TDbContext : DbContext
    {
        await using var scope = ServiceProvider!.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
        await using var ctx = scope.ServiceProvider.GetRequiredService<TDbContext>();
        await ctx.Database.MigrateAsync(cancellationToken);
    }

    #region IAsyncLifetime
    public virtual async Task DisposeAsync()
    {
        await _testDatabaseBuilder.DisposeAsync();
    }
    #endregion

    #region IDisposable
    public virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // do nothing
        }
        DisposeManagedResources();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract void DisposeManagedResources();

    ~IntegrationTestBaseFixture()
    {
        Dispose(false);
    }
    #endregion

    private class PostgreSqlTestDatabaseBuilder : IAsyncDisposable
    {
        //private readonly string? _dbImage;
        //private readonly string? _dbName;
        //private readonly string? _dbUsername;
        //private readonly string? _dbPassword;

        public PostgreSqlContainer Container { get; }

        public PostgreSqlTestDatabaseBuilder(
            string dbImage = "postgres:latest",
            string dbName = "test-db",
            string dbUsername = "test",
            string dbPassword = "test"
            )
        {
            //_dbImage = dbImage;
            //_dbName = dbName;
            //_dbUsername = dbUsername;
            //_dbPassword = dbPassword;

            Container = new PostgreSqlBuilder()
                .WithImage(dbImage)
                .WithDatabase(dbName)
                .WithUsername(dbUsername)
                .WithPassword(dbPassword)
                .Build();
        }

        public async ValueTask DisposeAsync()
        {
            await Container.DisposeAsync();
        }
    }

}
