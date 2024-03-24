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
        action.Should().Throw<InvalidOperationException>();
    }
}

internal class NotFreezableObjectException : Exception
{
}

internal class ObjectFrozenException : Exception
{
}

class Pet
{
    public int Age { get; set; }
    public bool Deceased { get; set; }
    public string Name { get; set; } = null!;
}

class Freezable
{
    public static bool IsFreezable<T>(T value) where T : class
    {
        throw new NotImplementedException();
    }

    public static T MakeFreezable<T>() where T : class
    {
        throw new NotImplementedException();
    }

    internal static void Freeze(Pet pet)
    {
        throw new NotImplementedException();
    }
}