using System.Collections;


namespace NoPrimitives.NewtonsoftJson.Tests.TestData;

public class ValueObjectsToJsonData : IEnumerable<object[]>
{
    private static readonly List<object[]> TestData =
    [
        new object[] { IntegerValueObject.Create(25), "25", typeof(IntegerValueObject) },
        new object[] { StringValueObject.Create("some value"), "\"some value\"", typeof(StringValueObject) },
        new object[]
        {
            DateTimeValueObject.Create(DateTime.Parse("12/24/2005")), "\"2005-12-24T00:00:00\"",
            typeof(DateTimeValueObject),
        },
    ];

    public IEnumerator<object[]> GetEnumerator() =>
        ValueObjectsToJsonData.TestData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}