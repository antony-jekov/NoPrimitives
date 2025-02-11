namespace NoPrimitives.Tests.UsageTests.GuidValueObject;

public class GuidValueObjectUsageTests
{
    private static readonly Guid RandomId = Guid.NewGuid();
    private readonly UserId _vo = UserId.Create(GuidValueObjectUsageTests.RandomId);

    [Fact]
    public void Value_HasCorrectValue()
    {
        this._vo.Value.Should().Be(GuidValueObjectUsageTests.RandomId);
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
        vo.Value.Should().Be(id);
    }

    [Fact]
    public void Comparable_ShouldCompareCorrectly()
    {
        var vo = UserId.Create(Guid.Parse("1c59fa8d-67a7-46e1-b987-86a7d58a26b0"));
        var lesser = UserId.Create(Guid.Parse("0c59fa8d-67a7-46e1-b987-86a7d58a26b0"));
        var greater = UserId.Create(Guid.Parse("2c59fa8d-67a7-46e1-b987-86a7d58a26b0"));
        var same = UserId.Create(Guid.Parse("1c59fa8d-67a7-46e1-b987-86a7d58a26b0"));

        vo.CompareTo(lesser).Should().BeGreaterThan(0);
        vo.CompareTo(greater).Should().BeLessThan(0);
        vo.CompareTo(same).Should().Be(0);
    }
}