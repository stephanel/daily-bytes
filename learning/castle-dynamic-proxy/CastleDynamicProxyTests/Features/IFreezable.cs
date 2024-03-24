namespace CastleDynamicProxyTests.Features;

interface IFreezable
{
    bool IsFrozen { get; }
    void Freeze();
}
