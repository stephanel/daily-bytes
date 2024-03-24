using Castle.DynamicProxy;
using System.Reflection;

namespace CastleDynamicProxyTests;

public class DynamicProxyTests
{
    [Fact]
    public void IsFreezable_should_be_false_for_objects_created_with_ctor()
    {
        var nonFreezablePet = new Pet();
        Assert.False(Freezable.IsFreezable(nonFreezablePet));
    }

    [Fact]
    public void IsFreezable_should_be_true_for_objects_created_with_MakeFreezable()
    {
        var freezablePet = Freezable.MakeFreezable<Pet>();
        Assert.True(Freezable.IsFreezable(freezablePet));
    }

    [Fact]
    public void Freezable_should_work_normally()
    {
        var pet = Freezable.MakeFreezable<Pet>();
        pet.Age = 3;
        pet.Deceased = true;
        pet.Name = "Rex";
        pet.Age += pet.Name.Length;
        Assert.NotNull(pet.ToString());
    }

    [Fact]
    public void Frozen_object_should_throw_ObjectFrozenException_when_trying_to_set_a_property()
    {
        var pet = Freezable.MakeFreezable<Pet>();
        pet.Age = 3;

        Freezable.Freeze(pet);

        Action act = () => pet.Name = "This should throw";
        act.Should().Throw<ObjectFrozenException>();
    }

    [Fact]
    public void Frozen_object_should_not_throw_when_trying_to_read_it()
    {
        var pet = Freezable.MakeFreezable<Pet>();
        pet.Age = 3;

        Freezable.Freeze(pet);

        var age = pet.Age;
        var name = pet.Name;
        var deceased = pet.Deceased;
        var str = pet.ToString();
    }

    [Fact]
    public void Freeze_nonFreezable_object_should_throw_NotFreezableObjectException()
    {
        var rex = new Pet();
        Action action = () => Freezable.Freeze(rex);
        action.Should().Throw<NotFreezableObjectException>();
    }
}

internal class NotFreezableObjectException : Exception
{
}

internal class ObjectFrozenException : Exception
{
}

public class Pet
{
    public virtual string Name { get; set; } = null!;
    public virtual int Age { get; set; } = 0;
    public virtual bool Deceased { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}, Deceased: {Deceased}";
    }
}

interface IFreezable
{
    bool IsFrozen { get; }
    void Freeze();
}

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

    public static bool IsFrozen(object value)
        => IsFreezable(value) && InstanceMap[value].IsFrozen;

    public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class, new()
    {
        var freezableInterceptor = new FreezableInterceptor();
        var proxy = Generator.CreateClassProxy<TFreezable>(new CallLoggingInterceptor(), freezableInterceptor);
        InstanceMap.Add(proxy, freezableInterceptor);
        return proxy;
    }
}

class CallLoggingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Intercepting: { invocation.Method}");
        invocation.Proceed();
    }
}

internal class FreezableInterceptor : IInterceptor, IFreezable
{
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
    }

    private static bool IsSetter(MethodInfo method)
        => method.IsSpecialName && method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);
}