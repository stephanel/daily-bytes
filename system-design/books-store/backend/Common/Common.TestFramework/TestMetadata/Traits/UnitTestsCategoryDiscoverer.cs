using Xunit.Sdk;

namespace BookStore.Common.TestFramework.TestMetadata.Traits;

public class UnitTestsCategoryDiscoverer : ITraitDiscoverer
{
    public const string VALUE = "UnitTests";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>(CategoryDiscoverer.KEY, VALUE);
    }
}

[TraitDiscoverer("BookStore.Common.TestFramework.TestMetadata.Traits.UnitTestsCategoryDiscoverer",
    "BookStore.Common.TestFramework")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class UnitTestAttribute() : Attribute, ITraitAttribute;


[TraitDiscoverer("BookStore.Common.TestFramework.TestMetadata.Traits.UnitTestsCategoryDiscoverer",
    "BookStore.Common.TestFramework")]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class UnitTestsAttribute() : Attribute, ITraitAttribute;
