using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;

namespace WebApi;

public class JsonSerializer<T> : ISerializer<T> where T : class
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        JsonSerializerOptions serializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            };

        var json = JsonSerializer.Serialize(data, serializerOptions)!;
        
        return Encoding.UTF8.GetBytes(json);
    }
}