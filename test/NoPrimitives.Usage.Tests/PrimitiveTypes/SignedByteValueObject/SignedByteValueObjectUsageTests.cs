namespace NoPrimitives.Usage.Tests.PrimitiveTypes.SignedByteValueObject;

public class SignedByteValueObjectUsageTests
{
    private readonly Height _vo = Height.Create(24);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((sbyte)this._vo.Value).Be(24);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Height.Create(24);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Height.Create(42);

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be("24");
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        sbyte voPrimitive = Height.Create(24);
        Height vo = voPrimitive;

        voPrimitive.Should().Be(24);
        AssertionExtensions.Should((sbyte)vo.Value).Be(24);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Height.Create(5);
        var greater = Height.Create(100);
        var same = Height.Create(24);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Height.Create(23);
        var greater = Height.Create(25);
        var same = Height.Create(24);

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