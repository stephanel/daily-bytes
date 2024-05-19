using Books.Infrastructure.Persistence.Migrations.Scripts;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Seed_Books : Migration
{
    private const string updateCustomerContractStartEventPath = "Scripts.SeedBooks.sql";

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            ScriptsReader.GetEmbeddedResourceTextContent(updateCustomerContractStartEventPath));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // do nothing
    }
}
