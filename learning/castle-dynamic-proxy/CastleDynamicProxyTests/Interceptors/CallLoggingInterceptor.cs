using Castle.DynamicProxy;
using CastleDynamicProxyTests.Features;

namespace CastleDynamicProxyTests.Interceptors;

internal class CallLoggingInterceptor : IInterceptor, IHasCount
{
    public int Count { get; private set; }

    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Intercepting: {invocation.Method}");
        invocation.Proceed();
        Count++;
    }
}
