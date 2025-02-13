namespace NoPrimitives.Usage.Tests.PrimitiveTypes.StringValueObject;

public class StringValueObjectUsageTests
{
    private readonly Email _vo = Email.Create("some@gmail.com");

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((string)this._vo.Value).Be("some@gmail.com");
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = Email.Create("some@gmail.com");

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = Email.Create("other@gmail.com");

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be("some@gmail.com");
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        string voPrimitive = Email.Create("some@gmail.com");
        Email vo = voPrimitive;

        voPrimitive.Should().Be("some@gmail.com");
        AssertionExtensions.Should((string)vo.Value).Be("some@gmail.com");
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var vo = Email.Create("bbb");
        var lesser = Email.Create("aaa");
        var greater = Email.Create("ccc");
        var same = Email.Create("bbb");

        AssertionExtensions.Should((int)vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)vo.CompareTo(same)).Be(0);
    }
}