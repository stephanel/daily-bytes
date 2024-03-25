using Castle.DynamicProxy;
using CastleDynamicProxyTests.Exceptions;
using CastleDynamicProxyTests.Features;
using System.Reflection;

namespace CastleDynamicProxyTests.Interceptors;

internal class FreezableInterceptor : IInterceptor, IFreezable, IHasCount
{
    public int Count { get; private set; }

    public void Freeze()
    {
        IsFrozen = true;
    }

    public bool IsFrozen { get; private set; }

    public void Intercept(IInvocation invocation)
    {
        if (IsFrozen && IsSetter(invocation.Method))
        {
            throw new ObjectFrozenException();
        }

        invocation.Proceed();
        Count++;
    }

    private static bool IsSetter(MethodInfo method)
        => method.IsSpecialName && method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);
}