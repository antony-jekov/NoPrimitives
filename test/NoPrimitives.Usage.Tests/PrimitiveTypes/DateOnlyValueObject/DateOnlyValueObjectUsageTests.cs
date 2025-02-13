using System.Globalization;


namespace NoPrimitives.Usage.Tests.PrimitiveTypes.DateOnlyValueObject;

public class DateOnlyValueObjectUsageTests
{
    private static readonly DateOnly Now = DateOnly.FromDateTime(DateTime.Now);
    private readonly EntryDate _vo = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((DateOnly)this._vo.Value).Be(DateOnlyValueObjectUsageTests.Now);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(1));

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be(DateOnlyValueObjectUsageTests.Now.ToString(CultureInfo.CurrentCulture));
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        DateOnly voPrimitive = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);
        EntryDate vo = voPrimitive;

        voPrimitive.Should().Be(DateOnlyValueObjectUsageTests.Now);
        AssertionExtensions.Should((DateOnly)vo.Value).Be(DateOnlyValueObjectUsageTests.Now);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(-1));
        var greater = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(1));
        var same = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(-1));
        var greater = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(1));
        var same = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);

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