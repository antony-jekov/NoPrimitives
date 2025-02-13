using System.Globalization;


namespace NoPrimitives.Usage.Tests.PrimitiveTypes.DateTimeValueObject;

public class DateTimeValueObjectUsageTests
{
    private static readonly DateTime Now = DateTime.UtcNow;
    private readonly Birthday _vo = Birthday.Create(DateTimeValueObjectUsageTests.Now);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((DateTime)this._vo.Value).Be(DateTimeValueObjectUsageTests.Now);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Birthday.Create(DateTimeValueObjectUsageTests.Now);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Birthday.Create(DateTime.Now.AddSeconds(1));

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be(DateTimeValueObjectUsageTests.Now.ToString(CultureInfo.CurrentCulture));
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        DateTime voPrimitive = Birthday.Create(DateTimeValueObjectUsageTests.Now);
        Birthday vo = voPrimitive;

        voPrimitive.Should().Be(DateTimeValueObjectUsageTests.Now);
        AssertionExtensions.Should((DateTime)vo.Value).Be(DateTimeValueObjectUsageTests.Now);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = Birthday.Create(DateTimeValueObjectUsageTests.Now.AddDays(-1));
        var greater = Birthday.Create(DateTimeValueObjectUsageTests.Now.AddDays(1));
        var same = Birthday.Create(DateTimeValueObjectUsageTests.Now);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = Birthday.Create(DateTimeValueObjectUsageTests.Now.AddDays(-1));
        var greater = Birthday.Create(DateTimeValueObjectUsageTests.Now.AddDays(1));
        var same = Birthday.Create(DateTimeValueObjectUsageTests.Now);

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