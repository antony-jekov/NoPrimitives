using System.Collections;


namespace NoPrimitives.NewtonsoftJson.Tests.TestData;

public class NullableValueObjectsToJsonData : IEnumerable<object[]>
{
    private static readonly Guid RandomGuid = Guid.NewGuid();

    private static readonly List<object[]> TestData =
    [
        [NullByteValueObject.Create(25), "25", typeof(NullByteValueObject)],
        [NullByteValueObject.Create(null), "null", typeof(NullByteValueObject)],
        [NullSByteValueObject.Create(26), "26", typeof(NullSByteValueObject)],
        [NullSByteValueObject.Create(null), "null", typeof(NullSByteValueObject)],
        [NullShortValueObject.Create(27), "27", typeof(NullShortValueObject)],
        [NullShortValueObject.Create(null), "null", typeof(NullShortValueObject)],
        [NullUShortValueObject.Create(28), "28", typeof(NullUShortValueObject)],
        [NullUShortValueObject.Create(null), "null", typeof(NullUShortValueObject)],
        [NullIntValueObject.Create(29), "29", typeof(NullIntValueObject)],
        [NullIntValueObject.Create(null), "null", typeof(NullIntValueObject)],
        [NullUIntValueObject.Create(30), "30", typeof(NullUIntValueObject)],
        [NullUIntValueObject.Create(null), "null", typeof(NullUIntValueObject)],
        [NullLongValueObject.Create(31), "31", typeof(NullLongValueObject)],
        [NullLongValueObject.Create(null), "null", typeof(NullLongValueObject)],
        [NullULongValueObject.Create(32), "32", typeof(NullULongValueObject)],
        [NullULongValueObject.Create(null), "null", typeof(NullULongValueObject)],
        [NullFloatValueObject.Create(33), "33.0", typeof(NullFloatValueObject)],
        [NullFloatValueObject.Create(null), "null", typeof(NullFloatValueObject)],
        [NullDoubleValueObject.Create(34), "34.0", typeof(NullDoubleValueObject)],
        [NullDoubleValueObject.Create(null), "null", typeof(NullDoubleValueObject)],
        [NullDecimalValueObject.Create(35), "35.0", typeof(NullDecimalValueObject)],
        [NullDecimalValueObject.Create(null), "null", typeof(NullDecimalValueObject)],
        [
            NullGuidValueObject.Create(NullableValueObjectsToJsonData.RandomGuid),
            $"\"{NullableValueObjectsToJsonData.RandomGuid.ToString()}\"", typeof(NullGuidValueObject),
        ],
        [
            NullDateTimeValueObject.Create(DateTime.Parse("12/24/2005")), "\"2005-12-24T00:00:00\"",
            typeof(NullDateTimeValueObject),
        ],
        [
            NullDateTimeOffsetValueObject.Create(
                new DateTimeOffset(2005, 12, 24, 0, 0, 0, TimeSpan.Zero)
            ),
            "\"2005-12-24T00:00:00+00:00\"",
            typeof(NullDateTimeOffsetValueObject),
        ],
        [
            NullDateOnlyValueObject.Create(
                new DateOnly(2005, 12, 24)
            ),
            "\"2005-12-24\"",
            typeof(NullDateOnlyValueObject),
        ],
        [
            NullTimeOnlyValueObject.Create(
                new TimeOnly(22, 12, 24)
            ),
            "\"22:12:24\"",
            typeof(NullTimeOnlyValueObject),
        ],
    ];

    public IEnumerator<object[]> GetEnumerator() =>
        NullableValueObjectsToJsonData.TestData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}