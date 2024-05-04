namespace ValidPalindrome.Benchmarks;

public class PalindromeGenerator
{
    private static readonly Random _random = new Random();
    private readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public string GenerateRandomPalindrome(int size)
    {
        char[] letters = new char[size];
        for (int i = 0; i < size / 2; i++)
        {
            char randomChar = GetRandomLetter();
            letters[i] = randomChar;
            letters[size - i - 1] = randomChar;
        }

        if (size % 2 != 0)
        {
            letters[size / 2] = GetRandomLetter();
        }

        return new string(letters);
    }

    private char GetRandomLetter()
    {
        int randomInt = _random.Next(0, chars.Length);
        char randomChar = chars[randomInt];
        return randomChar;
    }
}