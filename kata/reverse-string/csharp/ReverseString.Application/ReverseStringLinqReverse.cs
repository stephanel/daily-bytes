namespace ReverseString.Application
{
    internal class ReverseStringLinqReverse
    {
        internal string Reverse(string input)
            => string.Join(string.Empty, input.Reverse());
    }
}
