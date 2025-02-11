namespace NoPrimitives.Tests.UsageTests.IntegerValueObject;

public class IntegerValueObjectUsageTests
{
    private readonly Age _vo = Age.Create(24);

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be(24);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Age.Create(24);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Age.Create(42);

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
        int voPrimitive = Age.Create(24);
        Age vo = voPrimitive;

        voPrimitive.Should().Be(24);
        vo.Value.Should().Be(24);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Age.Create(5);
        var greater = Age.Create(100);
        var same = Age.Create(24);

        this._vo.CompareTo(lesser).Should().BeGreaterThan(0);
        this._vo.CompareTo(greater).Should().BeLessThan(0);
        this._vo.CompareTo(same).Should().Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Age.Create(23);
        var greater = Age.Create(25);
        var same = Age.Create(24);

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