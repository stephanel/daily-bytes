using Books.Infrastructure.Persistence;
using Common.TestFramework.Fixtures;

namespace Books.API.IntegrationTests.TestFramework.Context;

public class BooksWebApiFixture : WebApiFixture<Program>
{
    public BooksWebApiFixture(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
    { }

    protected override async Task RunDatabaseMigration() => await MigrateAsync<BooksDbContext>();
}
