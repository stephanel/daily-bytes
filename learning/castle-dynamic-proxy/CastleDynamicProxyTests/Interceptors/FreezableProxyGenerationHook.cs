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
        => methodInfo.IsSpecialName
        && (IsGetter(methodInfo) || IsSetter(methodInfo));

    private bool IsGetter(MethodInfo method)
        => method.Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase);

    private bool IsSetter(MethodInfo method)
        => method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);
}
