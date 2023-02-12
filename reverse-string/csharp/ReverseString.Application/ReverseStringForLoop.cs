using System.Text;

namespace ReverseString.Application
{
    internal class ReverseStringForLoop
    {
        internal string Reverse(string text)
        {
            StringBuilder builder = new();
            for (int i = text.Length - 1; i >= 0; i--)
            {
                builder.Append(text[i]);
            }
            return builder.ToString();
        }
    }
}
