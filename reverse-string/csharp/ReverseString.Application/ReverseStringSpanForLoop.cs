namespace ReverseString.Application
{
    internal class ReverseStringSpanForLoop
    {
        internal string Reverse(string input)
        {
            var span = new Span<char>(input.ToArray());

            for (int i = 0, j = span.Length - 1; i < j; i++, j--)
            {
                var temp = span[i];
                span[i] = span[j];
                span[j] = temp;
            }
            return span.ToString();
        }
    }
}
