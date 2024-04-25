using Modules.Communication.Tests.Asynchronous.Events;

namespace Modules.Communication.Tests.Synchronous;

public interface IModuleB
{
    void Do(DoCommand command);
}