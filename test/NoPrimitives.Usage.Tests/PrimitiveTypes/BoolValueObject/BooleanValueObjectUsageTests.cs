namespace NoPrimitives.Usage.Tests.PrimitiveTypes.BoolValueObject;

public class BooleanValueObjectUsageTests
{
    private readonly IsMale _vo =IsMale.Create(true);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((bool)this._vo.Value).Be(true);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same =IsMale.Create(true);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other =IsMale.Create(false);

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
        bool voPrimitive =IsMale.Create(true);
        IsMale vo = voPrimitive;

        voPrimitive.Should().BeTrue();
        AssertionExtensions.Should((bool)vo.Value).BeTrue();
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser =IsMale.Create(false);
        var same =IsMale.Create(true);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }
}