namespace ReverseString.Application
{
    internal class ReverseStringLinq
    {
        internal string ReverseLinq(string input)
            => string.Join(string.Empty, input.Reverse());
    }
}
