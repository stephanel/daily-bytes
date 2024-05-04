namespace CorrectCapitalization.Tests
{
    class AllLettersAreCapitalized : IRule
    {
        public bool Validate(string input) => input.All(c => char.IsUpper(c));
    }
}
