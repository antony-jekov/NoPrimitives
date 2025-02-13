namespace NoPrimitives.Usage.Tests.ParsingTests;

public class IntMethodParsingTests
{
    [Fact]
    public void TryParse_WhenValidIntString_ReturnsTrue()
    {
        bool isParsed = IntValueObject.TryParse("25", null, out IntValueObject? result);

        isParsed.Should().BeTrue();
        result.Value.Should().Be(25);
    }

    [Fact]
    public void TryParse_WhenInvalidIntString_ReturnsFalse()
    {
        bool isParsed = IntValueObject.TryParse("invalid int", null, out IntValueObject? result);

        isParsed.Should().BeFalse();
        result.Should().BeNull();
    }
}