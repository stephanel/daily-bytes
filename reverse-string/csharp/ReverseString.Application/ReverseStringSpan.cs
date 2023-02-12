namespace ReverseString.Application
{
    internal class ReverseStringSpan
    {
        internal string Reverse(string input)
        {
            var span = new Span<char>(input.ToArray());
            span.Reverse();
            return span.ToString();
        }
    }
}
