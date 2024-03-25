using Castle.DynamicProxy;
using System.Reflection;

namespace CastleDynamicProxyTests.Interceptors;

internal class FreezableInterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        if(IsSetter(method))
        {
            return interceptors;
        }
        return interceptors
            .Where(i => !(i is FreezableInterceptor))
            .ToArray();
    }
    private static bool IsSetter(MethodInfo method)
        => method.IsSpecialName && method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);

}