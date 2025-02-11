namespace NoPrimitives.Tests.UsageTests.DecimalValueObject;

public class DecimalValueObjectUsageTests
{
    private readonly Price _vo = Price.Create(5.5m);

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be(5.5m);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Price.Create(5.5m);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Price.Create(10.2m);

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be("5.5");
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        decimal voPrimitive = Price.Create(0.5m);
        Price vo = voPrimitive;

        voPrimitive.Should().Be(0.5m);
        vo.Value.Should().Be(0.5m);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Price.Create(4.55m);
        var greater = Price.Create(100.23m);
        var same = Price.Create(5.5m);

        this._vo.CompareTo(lesser).Should().BeGreaterThan(0);
        this._vo.CompareTo(greater).Should().BeLessThan(0);
        this._vo.CompareTo(same).Should().Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Price.Create(4.55m);
        var greater = Price.Create(100.23m);
        var same = Price.Create(5.5m);

        (this._vo > lesser).Should().BeTrue();
        (this._vo < greater).Should().BeTrue();

        (this._vo > same).Should().BeFalse();
        (this._vo < same).Should().BeFalse();

        (this._vo >= lesser).Should().BeTrue();
        (this._vo <= greater).Should().BeTrue();

        (this._vo >= same).Should().BeTrue();
        (this._vo <= same).Should().BeTrue();
    }
}