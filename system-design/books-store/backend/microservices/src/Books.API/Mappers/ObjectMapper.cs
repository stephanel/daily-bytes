namespace Books.API.Mappers;

internal static class ObjectMapper
{
    internal static TOut Map<TIn, TOut>(this TIn obj)
        where TIn : class
        where TOut : class
        => (TOut)Activator.CreateInstance(typeof(TOut), obj)!;
}
