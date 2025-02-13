using System.Globalization;


namespace NoPrimitives.Usage.Tests.PrimitiveTypes.DateTimeOffsetValueObject;

public class DateTimeOffsetValueObjectUsageTests
{
    private static readonly DateTimeOffset Now = DateTimeOffset.UtcNow;
    private readonly EntryTime _vo = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((DateTimeOffset)this._vo.Value).Be(DateTimeOffsetValueObjectUsageTests.Now);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = EntryTime.Create(DateTime.Now.AddSeconds(1));

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be(DateTimeOffsetValueObjectUsageTests.Now.ToString(CultureInfo.CurrentCulture));
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        DateTimeOffset voPrimitive = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now);
        EntryTime vo = voPrimitive;

        voPrimitive.Should().Be(DateTimeOffsetValueObjectUsageTests.Now);
        AssertionExtensions.Should((DateTimeOffset)vo.Value).Be(DateTimeOffsetValueObjectUsageTests.Now);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now.AddDays(-1));
        var greater = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now.AddDays(1));
        var same = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now);

        AssertionExtensions.Should((int)this._vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)this._vo.CompareTo(same)).Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now.AddDays(-1));
        var greater = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now.AddDays(1));
        var same = EntryTime.Create(DateTimeOffsetValueObjectUsageTests.Now);

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