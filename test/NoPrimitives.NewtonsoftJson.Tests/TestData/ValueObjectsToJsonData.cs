using System.Collections;


namespace NoPrimitives.NewtonsoftJson.Tests.TestData;

public class ValueObjectsToJsonData : IEnumerable<object[]>
{
    private static readonly List<object[]> TestData =
    [
        new object[] { IntegerValueObject.Create(25), "25", typeof(IntegerValueObject) },
    ];

    public IEnumerator<object[]> GetEnumerator() =>
        ValueObjectsToJsonData.TestData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}