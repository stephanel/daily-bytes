using Castle.DynamicProxy;
using CastleDynamicProxyTests.Exceptions;
using CastleDynamicProxyTests.Interceptors;

namespace CastleDynamicProxyTests.Features;

class Freezable
{
    private static readonly ProxyGenerator Generator = new ProxyGenerator();

    private static readonly Dictionary<object, IFreezable> InstanceMap = new();

    public static bool IsFreezable<TFreezable>(TFreezable value) where TFreezable : class, new()
        => value != null && InstanceMap.ContainsKey(value);

    public static void Freeze<T>(T value) where T : class, new()
    {
        if (!IsFreezable(value))
        {
            throw new NotFreezableObjectException();
        }

        InstanceMap[value].Freeze();
    }

    //public static bool IsFrozen(object value)
    //    => IsFreezable(value) && InstanceMap[value].IsFrozen;

    public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class, new()
    {
        var freezableInterceptor = new FreezableInterceptor();
        var proxy = Generator.CreateClassProxy<TFreezable>(new CallLoggingInterceptor(), freezableInterceptor);
        InstanceMap.Add(proxy, freezableInterceptor);
        return proxy;
    }
}
