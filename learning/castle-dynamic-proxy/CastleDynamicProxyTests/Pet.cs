namespace CastleDynamicProxyTests;

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
