namespace NoPrimitives.Usage.Tests.PrimitiveTypes.LongValueObject;

public class LongValueObjectUsageTests
{
    private readonly LogId _vo = LogId.Create(24);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((long)this._vo.Value).Be(24);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = LogId.Create(24);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = LogId.Create(42);

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
        long voPrimitive = LogId.Create(24);
        LogId vo = voPrimitive;

        voPrimitive.Should().Be(24);
        AssertionExtensions.Should((long)vo.Value).Be(24);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = LogId.Create(5);
        var greater = LogId.Create(100);
        var same = LogId.Create(24);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = LogId.Create(23);
        var greater = LogId.Create(25);
        var same = LogId.Create(24);

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