using Orders.API.IntegrationTests.TestFramework.Context;
using Common.TestFramework.TestContexts;
using Orders.API.DTOs;
using FastEndpoints;
using System.Net.Mime;
using System.Net.Http.Headers;
using System.Text;

namespace Orders.API.IntegrationTests.Endpoints;

[IntegrationTests]
public class PostItem : VerifyTestContext, IClassFixture<OrdersWebApiFixture>
{
    private readonly OrdersWebApiFixture _fixture;

    public PostItem(OrdersWebApiFixture fixture) : base(testOutputDirectory: "../Endpoints")
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Post_Item_Returns_Cookie_Session_When_Item_Exists()
    {
        var payload = """{ "Item": 1 }""";
        var response = await _fixture.Client.PostAsync(
            $"/api/orders/items",
            new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json),
            CancellationToken.None);
        IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
        await VerifyAsync(new { response, cookies });
    }

    //TODO: add mountebank

    [Fact]
    public async Task Post_Item_Returns_NotFound_When_Item_Does_Not_Exist()
    {
        var payload = """{ "Item": 999 }""";

        //_fixture.Client.DefaultRequestHeaders.Accept.Clear();
        //_fixture.Client.DefaultRequestHeaders.Accept.Append(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        var response = await _fixture.Client.PostAsync(
            $"/api/orders/items", 
            new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json),
            CancellationToken.None);
        await VerifyAsync(new { response, body = await response.Content.ReadAsStringAsync() });
    }

    [Fact]
    public async Task Get_Current_Order_Should_Return_Current_Order()
        => await VerifyHttpResponseMessageAsync<OrderDto>(
            await _fixture.Client.GetAsync($"/api/orders/current"));
}