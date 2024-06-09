using Common.TestFramework.TestContexts;
using Orders.API.DTOs;
using Orders.API.IntegrationTests.TestFramework.Collections;
using Orders.API.IntegrationTests.TestFramework.Context;
using Orders.Application.UseCases.AddItemToCurrentOrder;
using System.Net.Mime;
using System.Text;

namespace Orders.API.IntegrationTests.Endpoints;

[IntegrationTests]
[Collection(nameof(OrdersWebApiDependenciesCollection))]
public class PostItem : VerifyTestContext, IClassFixture<OrdersWebApiFixture>
{
    private readonly OrdersWebApiFixture _fixture;
    
    private int ItemId => 1;

    public PostItem(OrdersWebApiFixture fixture) : base(testOutputDirectory: "../Endpoints")
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Post_Item_Returns_Cookie_Session_When_Item_Exists()
    {
        await _fixture.MountebackFixture.StubGetEndpointAsync($"/books-service/books/{ItemId}", new Book(ItemId));
        var response = await CallAddOrderItemEndpoint(ItemId);
        IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
        await VerifyAsync(new { response, cookies });
    }

    [Fact]
    public async Task Post_Item_Returns_NotFound_When_Item_Does_Not_Exist()
    {
        int itemId = 1;
        await _fixture.MountebackFixture.StubEndpointNotFoundAsync($"/books-service/books/{itemId}");
        var response = await CallAddOrderItemEndpoint(itemId);
        await VerifyAsync(new { response, body = await response.Content.ReadAsStringAsync() });
    }

    private Task<HttpResponseMessage> CallAddOrderItemEndpoint(int itemId)
    {
        var payload = new StringContent($$"""{ "ItemId": {{itemId}} }""", Encoding.UTF8, MediaTypeNames.Application.Json);
        return _fixture.Client.PostAsync("/api/orders/items", payload, CancellationToken.None);
    }

    [Fact]
    public async Task Get_Current_Order_Should_Return_Current_Order()
        => await VerifyHttpResponseMessageAsync<OrderDto>(
            await _fixture.Client.GetAsync($"/api/orders/current"));
}