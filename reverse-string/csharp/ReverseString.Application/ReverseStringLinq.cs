namespace ReverseString.Application
{
    internal class ReverseStringLinq
    {
        internal string Reverse(string input)
            => string.Join(string.Empty, input.Reverse());
    }
}
