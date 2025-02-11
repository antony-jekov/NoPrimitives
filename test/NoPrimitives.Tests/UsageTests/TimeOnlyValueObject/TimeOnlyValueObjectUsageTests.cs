using System.Globalization;


namespace NoPrimitives.Tests.UsageTests.TimeOnlyValueObject;

public class TimeOnlyValueObjectUsageTests
{
    private static readonly TimeOnly Now = TimeOnly.FromDateTime(DateTime.Now);
    private readonly AlarmTime _vo = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now);

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be(TimeOnlyValueObjectUsageTests.Now);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now.AddMinutes(1));

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be(TimeOnlyValueObjectUsageTests.Now.ToString(CultureInfo.CurrentCulture));
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        TimeOnly voPrimitive = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now);
        AlarmTime vo = voPrimitive;

        voPrimitive.Should().Be(TimeOnlyValueObjectUsageTests.Now);
        vo.Value.Should().Be(TimeOnlyValueObjectUsageTests.Now);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var lesser = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now.AddMinutes(-1));
        var greater = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now.AddMinutes(1));
        var same = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now);

        this._vo.CompareTo(lesser).Should().BeGreaterThan(0);
        this._vo.CompareTo(greater).Should().BeLessThan(0);
        this._vo.CompareTo(same).Should().Be(0);
    }

    [Fact]
    public void LesserGreater_ShouldCompareCorrectly()
    {
        var lesser = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now.AddMinutes(-1));
        var greater = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now.AddMinutes(1));
        var same = AlarmTime.Create(TimeOnlyValueObjectUsageTests.Now);

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