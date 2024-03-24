using Castle.DynamicProxy;
using System.Reflection;

namespace CastleDynamicProxyTests.Interceptors;

internal class FreezableProxyGenerationHook : IProxyGenerationHook
{
    public void MethodsInspected()
    {
        // do nothing
    }

    public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
    {
        // do nothing
    }

    public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        => methodInfo.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);
}
