namespace NoPrimitives.Usage.Tests.ParsingTests;

public class GuidMethodParsingTests
{
    [Fact]
    public void TryParse_WhenValidGuidString_ReturnsTrue()
    {
        var guid = Guid.NewGuid();
        bool isParsed = GuidValueObject.TryParse(guid.ToString(), null, out GuidValueObject? result);

        isParsed.Should().BeTrue();
        result.Value.Should().Be(guid);
    }

    [Fact]
    public void TryParse_WhenInvalidGuidString_ReturnsFalse()
    {
        bool isParsed = GuidValueObject.TryParse("invalid guid", null, out GuidValueObject? result);

        isParsed.Should().BeFalse();
        result.Should().BeNull();
    }
}