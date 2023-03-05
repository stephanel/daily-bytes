namespace CorrectCapitalization.Tests
{
    class NoLettersAreCapitalized : IRule
    {
        public bool Validate(string input) => input.All(x => char.IsLower(x));
    }
}
