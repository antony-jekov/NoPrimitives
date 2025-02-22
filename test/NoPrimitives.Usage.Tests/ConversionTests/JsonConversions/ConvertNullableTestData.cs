using System.Collections;


namespace NoPrimitives.Usage.Tests.ConversionTests.JsonConversions;

public class ConvertNullableTestData : IEnumerable<object[]>
{
    private static readonly DateTime Now = new(2025, 2, 24, 11, 23, 48);
    private static readonly DateTimeOffset NowOffset = ConvertNullableTestData.Now;
    private static readonly DateOnly NowDateOnly = DateOnly.FromDateTime(ConvertNullableTestData.Now);
    private static readonly TimeOnly NowTimeOnly = TimeOnly.FromDateTime(ConvertNullableTestData.Now);

    private static readonly Guid RandomGuid = Guid.NewGuid();

    private static readonly List<object[]> TestData =
    [
        ["""{"SomeVo":20}""", NullByteValueObject.Create(20)],
        ["""{"SomeVo":null}""", NullByteValueObject.Create(null)],
        ["""{"SomeVo":25}""", NullSByteValueObject.Create(25)],
        ["""{"SomeVo":null}""", NullSByteValueObject.Create(null)],
        ["""{"SomeVo":30}""", NullShortValueObject.Create(30)],
        ["""{"SomeVo":null}""", NullShortValueObject.Create(null)],
        ["""{"SomeVo":35}""", NullUShortValueObject.Create(35)],
        ["""{"SomeVo":null}""", NullUShortValueObject.Create(null)],
        ["""{"SomeVo":40}""", NullIntValueObject.Create(40)],
        ["""{"SomeVo":null}""", NullIntValueObject.Create(null)],
        ["""{"SomeVo":41}""", NullUIntValueObject.Create(41)],
        ["""{"SomeVo":null}""", NullUIntValueObject.Create(null)],
        ["""{"SomeVo":42}""", NullLongValueObject.Create(42)],
        ["""{"SomeVo":null}""", NullLongValueObject.Create(null)],
        ["""{"SomeVo":43}""", NullULongValueObject.Create(43)],
        ["""{"SomeVo":null}""", NullULongValueObject.Create(null)],
        ["""{"SomeVo":44}""", NullFloatValueObject.Create(44)],
        ["""{"SomeVo":null}""", NullFloatValueObject.Create(null)],
        ["""{"SomeVo":45}""", NullDoubleValueObject.Create(45)],
        ["""{"SomeVo":null}""", NullDoubleValueObject.Create(null)],
        ["""{"SomeVo":46}""", NullDecimalValueObject.Create(46)],
        ["""{"SomeVo":null}""", NullDecimalValueObject.Create(null)],
        [
            $$"""{"SomeVo":"{{ConvertNullableTestData.RandomGuid}}"}""",
            NullGuidValueObject.Create(ConvertNullableTestData.RandomGuid),
        ],
        [
            """{"SomeVo":null}""",
            NullGuidValueObject.Create(null),
        ],
        [
            $$"""{"SomeVo":"{{ConvertNullableTestData.Now:yyyy-MM-ddThh:mm:ss}}"}""",
            NullDateTimeValueObject.Create(ConvertNullableTestData.Now),
        ],
        [
            """{"SomeVo":null}""",
            NullDateTimeValueObject.Create(null),
        ],
        [
            $$"""{"SomeVo":"{{ConvertNullableTestData.NowOffset:yyyy-MM-ddThh:mm:sszzz}}"}""",
            NullDateTimeOffsetValueObject.Create(ConvertNullableTestData.NowOffset),
        ],
        [
            """{"SomeVo":null}""",
            NullDateTimeOffsetValueObject.Create(null),
        ],
        [
            $$"""{"SomeVo":"{{ConvertNullableTestData.NowDateOnly:O}}"}""",
            NullDateOnlyValueObject.Create(ConvertNullableTestData.NowDateOnly),
        ],
        [
            """{"SomeVo":null}""",
            NullDateOnlyValueObject.Create(null),
        ],
        [
            $$"""{"SomeVo":"{{ConvertNullableTestData.NowTimeOnly:O}}"}""",
            NullTimeOnlyValueObject.Create(ConvertNullableTestData.NowTimeOnly),
        ],
        [
            """{"SomeVo":null}""",
            NullTimeOnlyValueObject.Create(null),
        ],
    ];

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();

    public IEnumerator<object[]> GetEnumerator() =>
        ConvertNullableTestData.TestData.GetEnumerator();
}