using Testcontainers.PostgreSql;

namespace Common.TestFramework.Fixtures;

public sealed class DatabaseFixture  : IAsyncLifetime, IDisposable
{
    private readonly PostgreSqlTestDatabaseBuilder _testDatabaseBuilder = new();

    public string? DatabaseConnectionString { get; private set; }

    /// <summary>
    /// This method is ionvoked by XUnit. Do not invoke directly.
    /// </summary>
    /// <returns></returns>
    public async Task InitializeAsync()
    {
        await _testDatabaseBuilder.Container.StartAsync();
        DatabaseConnectionString = _testDatabaseBuilder.Container.GetConnectionString();
    }

    #region IAsyncLifetime
    public async Task DisposeAsync()
    {
        await _testDatabaseBuilder.DisposeAsync();
    }
    #endregion

    #region IDisposable
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            // do nothing
        }
        // dispose managed resources
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~DatabaseFixture()
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