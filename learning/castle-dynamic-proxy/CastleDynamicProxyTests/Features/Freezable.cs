using Castle.DynamicProxy;
using CastleDynamicProxyTests.Exceptions;
using CastleDynamicProxyTests.Interceptors;

namespace CastleDynamicProxyTests.Features;

class Freezable
{
    private static readonly ProxyGenerator _generator = new ProxyGenerator();

    private static readonly IInterceptorSelector _interceptorSelector = new FreezableInterceptorSelector();

    public static bool IsFreezable(object obj) => AsFreezable(obj) != null;

    private static IFreezable AsFreezable(object target)
    {
        if (target == null)
            return null!;
        var hack = target as IProxyTargetAccessor;
        if (hack == null)
            return null!;
        return (hack.GetInterceptors().FirstOrDefault(i => i is FreezableInterceptor) as IFreezable)!;
    }

    public static bool IsFrozen(object obj)
    {
        var freezable = AsFreezable(obj);
        return freezable != null && freezable.IsFrozen;
    }

    public static void Freeze(object freezable)
    {
        var interceptor = AsFreezable(freezable);
        if (interceptor == null)
            throw new NotFreezableObjectException();
        interceptor.Freeze();
    }

    public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class, new()
    {
        var freezableInterceptor = new FreezableInterceptor();
        var options = new ProxyGenerationOptions(new FreezableProxyGenerationHook())
        {
            Selector = _interceptorSelector
        };
        var proxy = _generator.CreateClassProxy<TFreezable>(options, new CallLoggingInterceptor(), freezableInterceptor);
        return proxy;
    }
}
