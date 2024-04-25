

namespace Modules.Communication.Tests.Synchronous;

public class SynchronousCommunication
{
    [Fact]
    public void Test1()
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
