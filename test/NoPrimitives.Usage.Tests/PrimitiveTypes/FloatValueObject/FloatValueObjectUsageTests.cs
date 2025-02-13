namespace NoPrimitives.Usage.Tests.PrimitiveTypes.FloatValueObject;

public class FloatValueObjectUsageTests
{
    private readonly Average _vo = Average.Create(5.5f);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((float)this._vo.Value).Be(5.5f);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Average.Create(5.5f);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Average.Create(10.2f);

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
        float voPrimitive = Average.Create(0.5f);
        Average vo = voPrimitive;

        voPrimitive.Should().Be(0.5f);
        AssertionExtensions.Should((float)vo.Value).Be(0.5f);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Average.Create(4.5f);
        var greater = Average.Create(100.23f);
        var same = Average.Create(5.5f);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Average.Create(4.5f);
        var greater = Average.Create(100.23f);
        var same = Average.Create(5.5f);

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