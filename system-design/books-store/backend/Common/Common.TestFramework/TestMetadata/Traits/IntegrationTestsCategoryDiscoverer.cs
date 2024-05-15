using Xunit.Sdk;

namespace Common.TestFramework.TestMetadata.Traits;

public class IntegrationTestsCategoryDiscoverer : ITraitDiscoverer
{
    public const string VALUE = "IntegrationTests";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>(CategoryDiscoverer.KEY, VALUE);
    }
}

[TraitDiscoverer("Common.TestFramework.TestMetadata.Traits.IntegrationTestsCategoryDiscoverer",
    "Common.TestFramework")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class IntegrationTestAttribute : Attribute, ITraitAttribute;

[TraitDiscoverer("Common.TestFramework.TestMetadata.Traits.IntegrationTestsCategoryDiscoverer",
    "Common.TestFramework")]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class IntegrationTestsAttribute : Attribute, ITraitAttribute;