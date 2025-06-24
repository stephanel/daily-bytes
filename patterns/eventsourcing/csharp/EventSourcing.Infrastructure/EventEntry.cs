namespace EventSourcing.Infrastructure;

internal record EventEntry(DateTimeOffset CreatedOn, string DotNetType, string Payload, int Version);