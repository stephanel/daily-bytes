using Xunit.Sdk;

namespace BookStore.Common.TestFramework.TestMetadata.Traits;

public class IntegrationTestsCategoryDiscoverer : ITraitDiscoverer
{
    public const string VALUE = "IntegrationTest";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>(CategoryDiscoverer.KEY, VALUE);
    }
}

[TraitDiscoverer("BookStore.Common.TestFramework.TestMetadata.Traits.IntegrationTestsCategoryDiscoverer",
    "BookStore.Common.TestFramework")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class IntegrationTestsAttribute : Attribute, ITraitAttribute;