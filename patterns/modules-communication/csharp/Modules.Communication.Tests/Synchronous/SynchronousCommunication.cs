

namespace Modules.Communication.Tests.Synchronous;

public class SynchronousCommunication
{
    [Fact]
    public void API_Call()
    {
        // arrange

        var moduleB = new Mock<IModuleB>();
        var moduleA = new ModuleA(moduleB.Object);

        // assert
        moduleA.Do();

        // act
        moduleB.Verify(x => x.Do(), Times.Once);
    }
}
