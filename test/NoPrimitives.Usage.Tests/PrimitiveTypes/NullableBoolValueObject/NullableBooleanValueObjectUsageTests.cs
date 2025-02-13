namespace NoPrimitives.Usage.Tests.PrimitiveTypes.NullableBoolValueObject;

public class NullableBooleanValueObjectUsageTests
{
    private readonly Touched _vo = Touched.Create(true);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((bool?)this._vo.Value).Be(true);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Touched.Create(true);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Touched.Create(false);

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
        bool? voPrimitive = Touched.Create(true);
        Touched vo = voPrimitive;

        voPrimitive.Should().BeTrue();
        AssertionExtensions.Should((bool?)vo.Value).BeTrue();
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Touched.Create(false);
        var same = Touched.Create(true);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }
}