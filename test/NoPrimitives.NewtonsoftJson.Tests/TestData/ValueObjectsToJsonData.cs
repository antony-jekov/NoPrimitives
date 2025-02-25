using System.Collections;


namespace NoPrimitives.NewtonsoftJson.Tests.TestData;

public class ValueObjectsToJsonData : IEnumerable<object[]>
{
    private static readonly Guid RandomGuid = Guid.NewGuid();

    private static readonly List<object[]> TestData =
    [
        [ByteValueObject.Create(25), "25", typeof(ByteValueObject)],
        [SByteValueObject.Create(26), "26", typeof(SByteValueObject)],
        [ShortValueObject.Create(27), "27", typeof(ShortValueObject)],
        [UShortValueObject.Create(28), "28", typeof(UShortValueObject)],
        [IntValueObject.Create(29), "29", typeof(IntValueObject)],
        [UIntValueObject.Create(30), "30", typeof(UIntValueObject)],
        [LongValueObject.Create(31), "31", typeof(LongValueObject)],
        [ULongValueObject.Create(32), "32", typeof(ULongValueObject)],
        [FloatValueObject.Create(33), "33.0", typeof(FloatValueObject)],
        [DoubleValueObject.Create(34), "34.0", typeof(DoubleValueObject)],
        [DecimalValueObject.Create(35), "35.0", typeof(DecimalValueObject)],
        [StringValueObject.Create("some value"), "\"some value\"", typeof(StringValueObject)],
        [
            GuidValueObject.Create(ValueObjectsToJsonData.RandomGuid),
            $"\"{ValueObjectsToJsonData.RandomGuid.ToString()}\"", typeof(GuidValueObject),
        ],
        [
            DateTimeValueObject.Create(DateTime.Parse("12/24/2005")), "\"2005-12-24T00:00:00\"",
            typeof(DateTimeValueObject),
        ],
        [
            DateTimeOffsetValueObject.Create(
                new DateTimeOffset(2005, 12, 24, 0, 0, 0, TimeSpan.Zero)
            ),
            "\"2005-12-24T00:00:00+00:00\"",
            typeof(DateTimeOffsetValueObject),
        ],
        [
            DateOnlyValueObject.Create(
                new DateOnly(2005, 12, 24)
            ),
            "\"2005-12-24\"",
            typeof(DateOnlyValueObject),
        ],
        [
            TimeOnlyValueObject.Create(
                new TimeOnly(22, 12, 24)
            ),
            "\"22:12:24\"",
            typeof(TimeOnlyValueObject),
        ],
    ];

    public IEnumerator<object[]> GetEnumerator() =>
        ValueObjectsToJsonData.TestData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}