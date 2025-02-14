using System.Collections;
using Newtonsoft.Json;


namespace NoPrimitives.NewtonsoftJson.Tests;

public class SerializingConversionTests
{
    [Theory]
    [ClassData(typeof(ValueObjectsToJSONData))]
    public void Conversion_WhenValueObjectIsSerialized_ItIsSerializedAsItsPrimitive(object valueObject,
        string expectedJSON, Type expectedType)
    {
        string json = JsonConvert.SerializeObject(valueObject);

        json.Should().Be(expectedJSON);

        object? valueObjectDeserialized = JsonConvert.DeserializeObject(json, expectedType);

        valueObjectDeserialized.Should().NotBeNull();
        valueObjectDeserialized.Should().Be(valueObject);
    }
}

public class ValueObjectsToJSONData : IEnumerable<object[]>
{
    private static readonly List<object[]> TestData =
    [
        new object[] { IntegerValueObject.Create(25), "25", typeof(IntegerValueObject) },
    ];

    public IEnumerator<object[]> GetEnumerator() =>
        ValueObjectsToJSONData.TestData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}