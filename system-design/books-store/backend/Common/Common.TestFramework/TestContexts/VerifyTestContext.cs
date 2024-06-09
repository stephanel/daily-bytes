using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.TestFramework.TestContexts;

public abstract class VerifyTestContext
{
    private readonly VerifySettings _verifySettings = new();

    public VerifySettings VerifySettings => _verifySettings;

    protected VerifyTestContext(string? testOutputDirectory = null)
    {
        _verifySettings.AddExtraSettings(_ =>
        {
            _.DefaultValueHandling = Argon.DefaultValueHandling.Include;
            _.NullValueHandling = Argon.NullValueHandling.Ignore;
        });
        _verifySettings.DontScrubDateTimes();
        _verifySettings.DontScrubGuids();
        _verifySettings.DontIgnoreEmptyCollections();
        _verifySettings.DisableRequireUniquePrefix();

        // FIXME: make it better. if UseDirectory is not used, Verify generates
        // ouput files at the root level of the test assembly that own the executed tests
        // It happens since the verify settings were moved to this assembly
        if (!string.IsNullOrWhiteSpace(testOutputDirectory))
        {
            _verifySettings.UseDirectory(testOutputDirectory!);
        }
    }

    protected Task VerifyAsync<T>(T data) => Verify(data, _verifySettings);

    protected async Task VerifyHttpResponseMessageAsync<T>(HttpResponseMessage response)
        where T : class
    {
        var responseBody = await response.Content.ReadFromJsonAsync<T>(JsonSerializerOptions);

        // FIXME: refactor - need to control names of Verify output files
        await VerifyAsync(new { response, body = responseBody });
    }

    protected async Task VerifyHttpResponseCookieAsync<T>(HttpResponseMessage response)
    where T : class
    {
        IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
        await VerifyAsync(new { response, cookies });
    }

    protected JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
