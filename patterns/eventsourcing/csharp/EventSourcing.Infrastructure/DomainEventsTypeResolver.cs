using System.Runtime.CompilerServices;
using EventSourcing.Domain.Events;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using EventSourcing.Domain.Core;

namespace EventSourcing.Infrastructure;

public class DomainEventsTypeResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        Type baseType = typeof(IDomainEvent);
        if (jsonTypeInfo.Type == baseType)
        {
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$event-type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
            };

            GetDomainEventAsDerivedTypes().ForEach(jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add);
        }

        return jsonTypeInfo;
    }

    private static List<JsonDerivedType> GetDomainEventAsDerivedTypes()
        => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t =>
                typeof(IDomainEvent).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Select(t => new JsonDerivedType(t, t.Name)).ToList();
}