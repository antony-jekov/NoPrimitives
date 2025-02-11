namespace NoPrimitives.Tests.UsageTests.BoolValueObject;

public class BooleanValueObjectUsageTests
{
    private readonly IsMale _vo = IsMale.Create(true);

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be(true);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = IsMale.Create(true);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = IsMale.Create(false);

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be("True");
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        bool voPrimitive = IsMale.Create(true);
        IsMale vo = voPrimitive;

        voPrimitive.Should().BeTrue();
        vo.Value.Should().BeTrue();
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = IsMale.Create(false);
        var same = IsMale.Create(true);

        this._vo.CompareTo(lesser).Should().BeGreaterThan(0);
        this._vo.CompareTo(same).Should().Be(0);
    }
}