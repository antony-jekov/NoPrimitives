using System.Globalization;


namespace NoPrimitives.Tests.UsageTests.DateOnlyValueObject;

public class DateOnlyValueObjectUsageTests
{
    private static readonly DateOnly Now = DateOnly.FromDateTime(DateTime.Now);
    private readonly EntryDate _vo = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be(DateOnlyValueObjectUsageTests.Now);
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
        vo.Value.Should().Be(DateOnlyValueObjectUsageTests.Now);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(-1));
        var greater = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(1));
        var same = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);

        this._vo.CompareTo(lesser).Should().BeGreaterThan(0);
        this._vo.CompareTo(greater).Should().BeLessThan(0);
        this._vo.CompareTo(same).Should().Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(-1));
        var greater = EntryDate.Create(DateOnlyValueObjectUsageTests.Now.AddDays(1));
        var same = EntryDate.Create(DateOnlyValueObjectUsageTests.Now);

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