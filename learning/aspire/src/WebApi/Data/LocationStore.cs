using System.Reflection;
using Csv;
using WebApi.DTOs;

namespace WebApi.Data;

internal static class LocationStore
{
    private static readonly CsvDocument CsvDocumentCities;

    static LocationStore()
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebApi.Resources.cities.csv");
        using var ms = new MemoryStream();
        stream?.CopyTo(ms);
        CsvOptions options = new()
        {
            HasHeader = true,
            Separator = SeparatorType.Semicolon
        };
        CsvDocumentCities = CsvSerializer.ConvertToDocument(ms.ToArray(), options);
    }

    public static LocationDto PickRandom()  => CsvDocumentCities.Rows[Random.Shared.Next(CsvDocumentCities.Rows.Length)].ToLocationDto();
}

internal static class CsvRowExtensions
{
    public static LocationDto ToLocationDto(this CsvRow row) =>new(
        City: row[1].GetValue<string>()!,
        Country: row[7].GetValue<string>()!,
        CountryCode: row[6].GetValue<string>()!
    );
}