namespace NoPrimitives.Usage.Tests.PrimitiveTypes.UnsignedIntegerValueObject;

public class UnsignedIntegerValueObjectUsageTests
{
    private readonly Count _vo = Count.Create(24);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((uint)this._vo.Value).Be(24);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Count.Create(24);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Count.Create(42);

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
        uint voPrimitive = Count.Create(24);
        Count vo = voPrimitive;

        voPrimitive.Should().Be(24);
        AssertionExtensions.Should((uint)vo.Value).Be(24);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Count.Create(5);
        var greater = Count.Create(100);
        var same = Count.Create(24);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Count.Create(23);
        var greater = Count.Create(25);
        var same = Count.Create(24);

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