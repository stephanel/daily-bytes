using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consumer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCityCountryCountryCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WeatherForecasts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "WeatherForecasts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "WeatherForecasts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "WeatherForecasts");
        }
    }
}
