namespace NoPrimitives.Usage.Tests.PrimitiveTypes.CharValueObject;

public class CharValueObjectUsageTests
{
    private readonly Letter _vo = Letter.Create('b');

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be('b');
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Letter.Create('b');

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Letter.Create('z');

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be("b");
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        char voPrimitive = Letter.Create('x');
        Letter vo = voPrimitive;

        voPrimitive.Should().Be('x');
        vo.Value.Should().Be('x');
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Letter.Create('a');
        var greater = Letter.Create('c');
        var same = Letter.Create('b');

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }
}