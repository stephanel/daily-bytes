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
        var method = memberInfo as MethodInfo;
        if (method != null)
        {
            ValidateNoSetter(method);
        }
    }

    public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        => methodInfo.IsSpecialName
        && (IsGetter(methodInfo) || IsSetter(methodInfo));

    private void ValidateNoSetter(MethodInfo methodInfo)
    {
        if(methodInfo.IsSpecialName && IsSetter(methodInfo))
        {
            throw new InvalidOperationException(
                $"Property {methodInfo.Name.Substring("set_".Length)} is not virtual. Can't freeze classes with non-virtual properties.");
        }
    }

    private bool IsGetter(MethodInfo method)
        => method.Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase);

    private bool IsSetter(MethodInfo method)
        => method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);
}
