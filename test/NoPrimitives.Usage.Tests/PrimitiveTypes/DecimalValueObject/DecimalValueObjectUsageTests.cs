namespace NoPrimitives.Usage.Tests.PrimitiveTypes.DecimalValueObject;

public class DecimalValueObjectUsageTests
{
    private readonly Price _vo = Price.Create(5.5m);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((decimal)this._vo.Value).Be(5.5m);
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
        AssertionExtensions.Should((decimal)vo.Value).Be(0.5m);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Price.Create(4.55m);
        var greater = Price.Create(100.23m);
        var same = Price.Create(5.5m);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Price.Create(4.55m);
        var greater = Price.Create(100.23m);
        var same = Price.Create(5.5m);

        AssertionExtensions.Should((bool)(this._vo > lesser)).BeTrue();
        AssertionExtensions.Should((bool)(this._vo < greater)).BeTrue();

        AssertionExtensions.Should((bool)(this._vo > same)).BeFalse();
        AssertionExtensions.Should((bool)(this._vo < same)).BeFalse();

        AssertionExtensions.Should((bool)(this._vo >= lesser)).BeTrue();
        AssertionExtensions.Should((bool)(this._vo <= greater)).BeTrue();

        AssertionExtensions.Should((bool)(this._vo >= same)).BeTrue();
        AssertionExtensions.Should((bool)(this._vo <= same)).BeTrue();
    }
}