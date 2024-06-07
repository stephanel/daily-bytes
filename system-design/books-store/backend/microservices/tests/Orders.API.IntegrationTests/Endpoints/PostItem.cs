using Orders.API.IntegrationTests.TestFramework.Context;
using Common.TestFramework.TestContexts;
using Orders.API.DTOs;

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
    public async Task Post_Item_Should_Return_Cookie_Session_When_Item_Exists()
    {
        var response = await _fixture.Client.PostAsync($"/api/orders/items/1", null);
        IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
        await VerifyAsync(new { response, cookies });
    }

    [Fact]
    public async Task Get_Current_Order_Should_Return_Current_Order()
        => await VerifyHttpResponseMessageAsync<OrderDto>(
            await _fixture.Client.GetAsync($"/api/orders/current"));
}