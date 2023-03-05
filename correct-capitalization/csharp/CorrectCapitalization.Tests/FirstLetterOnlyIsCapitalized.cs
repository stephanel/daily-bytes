namespace CorrectCapitalization.Tests
{
    class FirstLetterOnlyIsCapitalized : IRule
    {
        private readonly IRule allLettersAreCapitalized;
        private readonly IRule noLettersAreCapitalized;

        public FirstLetterOnlyIsCapitalized(
            IRule allLettersAreCapitalized,
            IRule noLettersAreCapitalized)
        {
            this.allLettersAreCapitalized = allLettersAreCapitalized;
            this.noLettersAreCapitalized = noLettersAreCapitalized;
        }

        public bool Validate(string input)
            => allLettersAreCapitalized.Validate(input.First().ToString())
            && noLettersAreCapitalized.Validate(string.Join(string.Empty, input.Skip(1)));
    }
}