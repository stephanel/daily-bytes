

namespace Modules.Communication.Tests.Synchronous;

public record ModuleA(IModuleB moduleB)
{
    internal void Do()
    {
        // synchronous call to ModuleB's public API
        moduleB.Do();
    }
}
