namespace NoPrimitives.Usage.Tests.ParsingTests;

public class NullableIntMethodParsingTests
{
    [Fact]
    public void TryParse_WhenValidIntString_ReturnsTrue()
    {
        bool isParsed = NullableIntValueObject.TryParse("25", null, out NullableIntValueObject? result);

        isParsed.Should().BeTrue();
        result.Value.Should().Be(25);
    }

    [Fact]
    public void TryParse_WhenInvalidIntString_ReturnsFalse()
    {
        bool isParsed = NullableIntValueObject.TryParse("invalid int", null, out NullableIntValueObject? result);

        isParsed.Should().BeFalse();
        result.Should().BeNull();
    }
}