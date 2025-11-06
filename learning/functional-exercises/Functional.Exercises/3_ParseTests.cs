namespace Functional.Exercises;

internal class _3_ParseTests
{
    [Test]
    public async Task Enum_Parse_returns_value_when_string_is_valid_enum_name()
    {
        await Assert.That("Monday".Parse<DayOfWeek>()).IsEqualTo(DayOfWeek.Monday);
        await Assert.That("Funday".Parse<DayOfWeek>()).IsEqualTo(None);
    }
}
