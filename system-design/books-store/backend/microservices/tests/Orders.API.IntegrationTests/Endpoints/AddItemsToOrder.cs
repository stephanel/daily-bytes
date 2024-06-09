using Common.TestFramework.TestContexts;
using Orders.API.DTOs;
using Orders.API.IntegrationTests.TestFramework.Collections;
using Orders.API.IntegrationTests.TestFramework.Context;
using Orders.API.IntegrationTests.TestFramework.Fakes;
using Orders.Application.UseCases.AddItemToCurrentOrder;
using Orders.Infrastructure.ExternalServices;
using System.Net.Mime;
using System.Text;

namespace Orders.API.IntegrationTests.Endpoints;

[IntegrationTests]
[Collection(nameof(OrdersWebApiDependenciesCollection))]
public class AddItemsToOrder(OrdersWebApiFixture fixture)
    : VerifyTestContext(testOutputDirectory: "../Endpoints"), 
    IClassFixture<OrdersWebApiFixture>
{
    private readonly OrdersWebApiFixture _fixture = fixture;

    private readonly SessionId _sessionId = "52055aee-b822-4db8-a14a-3b4074c51c5c";

    private int ItemId => 1;

    private StringContent Payload => new(
        $@"{{ ""ItemId"": {ItemId} }}", 
        Encoding.UTF8, 
        MediaTypeNames.Application.Json);

    [Fact]
    public async Task Post_Item_Returns_Cookie_Session_When_Item_Exists()
    {
        // Arrange
        await _fixture.MountebackFixture.StubGetEndpointAsync($"/books-service/books/{ItemId}", new Book(ItemId));

        var sessionIdGenerator = (FakeSessionIdGenerator)_fixture.GetRequiredService<ISessionIdGenerator>();
        sessionIdGenerator.SeedNext(_sessionId);

        // Act
        var response = await CallAddOrderItemEndpoint();

        // Assert
        await VerifyAsync(new 
        { 
            response, 
            cookies = response
                .Headers
                .SingleOrDefault(header => header.Key == "Set-Cookie")
                .Value
        });
    }

    [Fact]
    public async Task Post_Item_Returns_NotFound_When_Item_Does_Not_Exist()
    {
        // Arrange
        int itemId = 1;
        await _fixture.MountebackFixture.StubEndpointNotFoundAsync($"/books-service/books/{itemId}");

        // Act
        // Assert
         await VerifyHttpResponseMessageAsync<ApiResponseFailure>(
            await CallAddOrderItemEndpoint());
    }

    private Task<HttpResponseMessage> CallAddOrderItemEndpoint()
        => _fixture.Client.PostAsync(
            "/api/orders/items", 
            Payload, 
            CancellationToken.None);
}

class ApiResponseFailure
{
    public int? StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Errors { get; set; }
}