namespace Modules.Communication.Tests.Asynchronous;

public record ModuleA()
{
    internal void Do()
    {
        // synchronous call to ModuleB's public API
        moduleB.Do();
    }
}
