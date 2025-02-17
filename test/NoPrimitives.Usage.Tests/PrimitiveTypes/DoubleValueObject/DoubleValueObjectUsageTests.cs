namespace NoPrimitives.Usage.Tests.PrimitiveTypes.DoubleValueObject;

public class DoubleValueObjectUsageTests
{
    private readonly Percent _vo = Percent.Create(5.5d);

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be(5.5d);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Percent.Create(5.5d);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Percent.Create(10.2d);

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
        double voPrimitive = Percent.Create(0.5d);
        Percent vo = voPrimitive;

        voPrimitive.Should().Be(0.5d);
        vo.Value.Should().Be(0.5d);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Percent.Create(4.5d);
        var greater = Percent.Create(100.23d);
        var same = Percent.Create(5.5d);

        this._vo.CompareTo(lesser).Should().BeGreaterThan(0);
        this._vo.CompareTo(greater).Should().BeLessThan(0);
        this._vo.CompareTo(same).Should().Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Percent.Create(4.5d);
        var greater = Percent.Create(100.23d);
        var same = Percent.Create(5.5d);

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