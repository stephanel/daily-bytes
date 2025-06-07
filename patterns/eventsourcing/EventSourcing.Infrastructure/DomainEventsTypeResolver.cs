using EventSourcing.Domain.Events;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

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
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(BookingCreated), nameof(BookingCreated)),
                    new JsonDerivedType(typeof(BookedTimeSlotChanged), nameof(BookedTimeSlotChanged)),
                    new JsonDerivedType(typeof(MeetingSpaceChanged), nameof(MeetingSpaceChanged))
                }
            };
        }

        return jsonTypeInfo;
    }
}