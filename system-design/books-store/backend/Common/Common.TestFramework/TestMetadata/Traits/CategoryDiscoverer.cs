﻿using Xunit.Sdk;

namespace BookStore.Common.TestFramework.TestMetadata.Traits;

public class CategoryDiscoverer : ITraitDiscoverer
{
    public const string KEY = "Category";

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var ctorArgs = traitAttribute.GetConstructorArguments().ToList();
        yield return new KeyValuePair<string, string>(KEY, ctorArgs[0].ToString()!);
    }
}

//NOTICE: Take a note that you must provide appropriate namespace here
[TraitDiscoverer("BookStore.Common.TestFramework.TestMetadata.Traits.CategoryDiscoverer",
    "BookStore.Common.TestFramework")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CategoryAttribute : Attribute, ITraitAttribute
{
    public CategoryAttribute(string category) { }
}