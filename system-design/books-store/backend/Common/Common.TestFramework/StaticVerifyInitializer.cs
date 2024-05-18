using System.Runtime.CompilerServices;

namespace Common.TestFramework;

public static class StaticVerifyInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.UseStrictJson();
        VerifyDiffPlex.Initialize(VerifyTests.DiffPlex.OutputType.Compact);
    }
}
