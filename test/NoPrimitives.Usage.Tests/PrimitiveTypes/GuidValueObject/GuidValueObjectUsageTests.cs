namespace NoPrimitives.Usage.Tests.PrimitiveTypes.GuidValueObject;

public class GuidValueObjectUsageTests
{
    private static readonly Guid RandomId = Guid.NewGuid();
    private readonly UserId _vo = UserId.Create(GuidValueObjectUsageTests.RandomId);

    [Fact]
    public void Value_HasCorrectValue()
    {
        AssertionExtensions.Should((Guid)this._vo.Value).Be(GuidValueObjectUsageTests.RandomId);
    }

    [Fact]
    public void ValueObject_WhenComparedByOperator_ComparesCorrectly()
    {
        var same = UserId.Create(GuidValueObjectUsageTests.RandomId);

        (this._vo == same).Should().BeTrue();
        (this._vo != same).Should().BeFalse();

        var other = UserId.Create(Guid.NewGuid());

        (this._vo == other).Should().BeFalse();
        (this._vo != other).Should().BeTrue();
    }

    [Fact]
    public void ToString_ReturnsToStringOfThePrimitive()
    {
        this._vo.ToString().Should().Be(GuidValueObjectUsageTests.RandomId.ToString());
    }

    [Fact]
    public void ImplicitOperator_ConvertsToAndFromString()
    {
        var id = Guid.NewGuid();
        Guid voPrimitive = UserId.Create(id);
        UserId vo = voPrimitive;

        voPrimitive.Should().Be(id);
        AssertionExtensions.Should((Guid)vo.Value).Be(id);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var vo = UserId.Create(Guid.Parse("1c59fa8d-67a7-46e1-b987-86a7d58a26b0"));
        var lesser = UserId.Create(Guid.Parse("0c59fa8d-67a7-46e1-b987-86a7d58a26b0"));
        var greater = UserId.Create(Guid.Parse("2c59fa8d-67a7-46e1-b987-86a7d58a26b0"));
        var same = UserId.Create(Guid.Parse("1c59fa8d-67a7-46e1-b987-86a7d58a26b0"));

        AssertionExtensions.Should((int)vo.CompareTo(lesser)).BeGreaterThan(0);
        AssertionExtensions.Should((int)vo.CompareTo(greater)).BeLessThan(0);
        AssertionExtensions.Should((int)vo.CompareTo(same)).Be(0);
    }
}