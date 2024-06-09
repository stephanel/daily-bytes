using Books.API.IntegrationTests.TestFramework.Collections;
using Books.Infrastructure.Persistence;
using Common.TestFramework.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Books.API.IntegrationTests.TestFramework.Context;

[Collection(nameof(BooksWebApiDependenciesCollection))]
public class BooksWebApiFixture : WebApiFixture<Program>, IAsyncLifetime
{
    private readonly DatabaseFixture _databaseFixture;

    protected override IReadOnlyDictionary<string, string?> AppSettings => new Dictionary<string, string?>
    {
        ["ConnectionStrings:BooksDatabaseConnectionString"] = _databaseFixture.DatabaseConnectionString,
    };

    public BooksWebApiFixture(DatabaseFixture databaseFixture, IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
    {
        _databaseFixture = databaseFixture;
    }

    public async Task InitializeAsync()
    {
        await using var scope = GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
        await using var ctx = scope.ServiceProvider.GetRequiredService<BooksDbContext>();
        await ctx.Database.MigrateAsync(CancellationToken.None);
    }

    Task IAsyncLifetime.DisposeAsync() => Task.CompletedTask;
}
