using Castle.DynamicProxy;

namespace CastleDynamicProxyTests.Interceptors;

class CallLoggingInterceptor : IInterceptor
{
    public int Count { get; private set; }

    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Intercepting: {invocation.Method}");
        invocation.Proceed();
        Count++;
    }
}
