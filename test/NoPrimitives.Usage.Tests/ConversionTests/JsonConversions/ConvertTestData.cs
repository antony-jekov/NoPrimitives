using System.Collections;


namespace NoPrimitives.Usage.Tests.ConversionTests.JsonConversions;

public class ConvertTestData : IEnumerable<object[]>
{
    private static readonly DateTime Now = new(2025, 2, 24, 11, 23, 48);
    private static readonly DateTimeOffset NowOffset = ConvertTestData.Now;
    private static readonly DateOnly NowDateOnly = DateOnly.FromDateTime(ConvertTestData.Now);
    private static readonly TimeOnly NowTimeOnly = TimeOnly.FromDateTime(ConvertTestData.Now);

    private static readonly Guid RandomGuid = Guid.NewGuid();

    private static readonly List<object[]> TestData =
    [
        ["""{"SomeVo":20}""", ByteValueObject.Create(20)],
        ["""{"SomeVo":25}""", SByteValueObject.Create(25)],
        ["""{"SomeVo":30}""", ShortValueObject.Create(30)],
        ["""{"SomeVo":35}""", UShortValueObject.Create(35)],
        ["""{"SomeVo":40}""", IntValueObject.Create(40)],
        ["""{"SomeVo":41}""", UIntValueObject.Create(41)],
        ["""{"SomeVo":42}""", LongValueObject.Create(42)],
        ["""{"SomeVo":43}""", ULongValueObject.Create(43)],
        ["""{"SomeVo":44}""", FloatValueObject.Create(44)],
        ["""{"SomeVo":45}""", DoubleValueObject.Create(45)],
        ["""{"SomeVo":46}""", DecimalValueObject.Create(46)],
        ["""{"SomeVo":"some string"}""", StringValueObject.Create("some string")],
        [
            $$"""{"SomeVo":"{{ConvertTestData.RandomGuid}}"}""",
            GuidValueObject.Create(ConvertTestData.RandomGuid),
        ],
        [
            $$"""{"SomeVo":"{{ConvertTestData.Now:yyyy-MM-ddThh:mm:ss}}"}""",
            DateTimeValueObject.Create(ConvertTestData.Now),
        ],
        [
            $$"""{"SomeVo":"{{ConvertTestData.NowOffset:yyyy-MM-ddThh:mm:sszzz}}"}""",
            DateTimeOffsetValueObject.Create(ConvertTestData.NowOffset),
        ],
        [
            $$"""{"SomeVo":"{{ConvertTestData.NowDateOnly:O}}"}""",
            DateOnlyValueObject.Create(ConvertTestData.NowDateOnly),
        ],
        [
            $$"""{"SomeVo":"{{ConvertTestData.NowTimeOnly:O}}"}""",
            TimeOnlyValueObject.Create(ConvertTestData.NowTimeOnly),
        ],
    ];

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();

    public IEnumerator<object[]> GetEnumerator() =>
        ConvertTestData.TestData.GetEnumerator();
}