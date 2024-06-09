using Common.TestFramework.TestContexts;
using Orders.API.DTOs;
using Orders.API.IntegrationTests.TestFramework.Collections;
using Orders.API.IntegrationTests.TestFramework.Context;

namespace Orders.API.IntegrationTests.Endpoints;

[IntegrationTests]
[Collection(nameof(OrdersWebApiDependenciesCollection))]
public class GetOrder : VerifyTestContext, IClassFixture<OrdersWebApiFixture>
{
    private readonly OrdersWebApiFixture _fixture;
    
    public GetOrder(OrdersWebApiFixture fixture) : base(testOutputDirectory: "../Endpoints")
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Get_Current_Order_Should_Return_Current_Order()
        => await VerifyHttpResponseMessageAsync<OrderDto>(
            await _fixture.Client.GetAsync($"/api/orders/current"));
}