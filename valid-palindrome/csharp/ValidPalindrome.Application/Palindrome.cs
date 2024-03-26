using System.Text.RegularExpressions;

namespace ValidPalindrome.Application;

internal class Palindrome
{
    static Regex rgx = new Regex("[^a-zA-Z-]"); // to remove all except alpha characters
    public bool ValidateUsingRegexForLoop(string input)
    {
        var cleaned = rgx.Replace(input, string.Empty).ToLower();
        var reverted = cleaned.Reverse();
        return Enumerable.SequenceEqual(cleaned, reverted);
    }

    public bool ValidateUsingAsciiCodeLinq(string input)
    {
        var cleaned = CleanInput(input);
        return Enumerable.SequenceEqual(cleaned, cleaned.Reverse());
    }

    public bool ValidateUsingAsciiCodeForLoop(string input)
    {
        var cleaned = CleanInput(input);

        var midLength = (cleaned.Length - 1) / 2;
        for (int i = 0; i <= midLength; i++)
        {
            var j = cleaned.Length - 1 - i;
            if (cleaned[i] != cleaned[j])
            {
                return false;
            }
        }
        return true;
    }

    public bool ValidateUsingAsciiCodeForLoopAlternativeLogic(string input)
    {
        var cleaned = CleanInput(input);

        var length = cleaned.Length;
        var isEven = (length % 2) == 0;
        var midLength = (length - 1) / 2;

        if (!isEven)
            midLength--;

        var evenDelta = isEven ? 1 : 2;

        for (int i = midLength, j = midLength + evenDelta;
            i >= 0;
            i--, j++)
        {
            if (cleaned[i] != cleaned[j])
            {
                return false;
            }
        }

        return true;
    }

    private char[] CleanInput(string input)
        => input
        .Where(char.IsAsciiLetter)
        .Select(ConvertToLower)
        .ToArray();

    public char ConvertToLower(char c)
        => (short)c >= 65 && (short)c <= 90   // c in [A..Z]
            ? (char)((short)c + 32)   // tolower
            : c;
}
