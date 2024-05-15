namespace Books.API.Mappers;

internal static class EnumMapper
{
    internal static TDestination Map<TOrigin, TDestination>(this TOrigin originEnum)
        where TOrigin : struct, Enum
        where TDestination : struct, Enum
        => Enum.TryParse<TDestination>(originEnum.ToString(), out var destinationEnum)
            ? destinationEnum
            : throw new ArgumentNullException(originEnum.ToString());
}
