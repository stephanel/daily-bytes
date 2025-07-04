namespace EventSourcing.Infrastructure;

internal record EventEntry(DateTimeOffset CreatedOn, string DotNetType, string Payload, int Version)
{
    public static EventEntry Empty => new(default, null!, null!, 0);
}