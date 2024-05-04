using CSharp.ChainOfResponsibility.Tests.Fakes;
using CSharp.ChainOfResponsibility.Tests.Handlers;
using CSharp.ChainOfResponsibility.Tests.Messages;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CSharp.ChainOfResponsibility.Tests;

public class ChainOfResponsibilityTests
{
    [Fact]
    public void ChainOfResponsibility_NoDependencyInjection()
    {
        // arrange
        FakeOrderNotification orderNotification = new();  // create a mock to verify calls
        OrderPersistence orderPersistence = new(orderNotification);
        OrderValidation orderValidation = new(orderPersistence);

        OrderProcessHandler handler = new(orderValidation);

        // act
        OrderReceivedMessage message = new("Order#001234");
        handler!.Handle(message);

        // assert
        orderNotification.ReceivedMessage.Should().BeEquivalentTo(message);
    }

    [Fact]
    public void ChainOfResponsibility_DependencyInjection()
    {
        FakeOrderNotification orderNotification = new();  // create a mock to verify calls

        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddKeyedSingleton<IHandler, OrderNotification>(nameof(OrderNotification), (sp, key) => orderNotification)
            .AddKeyedSingleton<IHandler, OrderPersistence>(nameof(OrderPersistence), (sp, key) =>
            {
                IHandler orderNotification = sp.GetRequiredKeyedService<IHandler>(nameof(OrderNotification));
                return new OrderPersistence(orderNotification);
            })
            .AddKeyedSingleton<IHandler, OrderValidation>(nameof(OrderValidation), (sp, key) => 
            {
                IHandler orderPersistence = sp.GetRequiredKeyedService<IHandler>(nameof(OrderPersistence));
                return new OrderValidation(orderPersistence);
            })
            .AddSingleton<OrderProcessHandler>();

        var services = serviceCollection.BuildServiceProvider();

        var handler = services.GetRequiredService<OrderProcessHandler>();

        // act
        OrderReceivedMessage message = new("Order#001234");
        handler!.Handle(message);

        // assert
        orderNotification.ReceivedMessage.Should().BeEquivalentTo(message);
    }
}
