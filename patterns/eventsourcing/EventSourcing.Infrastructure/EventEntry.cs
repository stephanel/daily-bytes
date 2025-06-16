namespace EventSourcing.Infrastructure;

internal record EventEntry(string DotNetType, string Payload, int Version);