namespace NoPrimitives.Usage.Tests.ParsingTests;

public class DateTimeParsingTests
{
    [Fact]
    public void TryParse_WhenValidDateTimeString_ReturnsTrue()
    {
        var nowString = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        bool isParsed =
            DateTimeValueObject.TryParse(nowString, null, out DateTimeValueObject? result);

        DateTime expected = DateTime.Parse(nowString);

        isParsed.Should().BeTrue();
        result.Value.Should().Be(expected);
    }

    [Fact]
    public void TryParse_WhenInvalidDateTimeString_ReturnsFalse()
    {
        bool isParsed = DateTimeValueObject.TryParse("invalid date", null, out DateTimeValueObject? result);

        isParsed.Should().BeFalse();
        result.Should().BeNull();
    }
}