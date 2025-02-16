using Newtonsoft.Json;
using NoPrimitives.NewtonsoftJson.Tests.TestData;


namespace NoPrimitives.NewtonsoftJson.Tests;

public class SerializingConversionTests
{
    [Theory]
    [ClassData(typeof(ValueObjectsToJsonData))]
    public void Conversion_WhenValueObjectIsSerialized_ItIsSerializedAsItsPrimitive(object valueObject,
        string expectedJson, Type expectedType)
    {
        string json = JsonConvert.SerializeObject(valueObject);

        json.Should().Be(expectedJson);

        object? valueObjectDeserialized = JsonConvert.DeserializeObject(json, expectedType);

        valueObjectDeserialized.Should().NotBeNull();
        valueObjectDeserialized.Should().Be(valueObject);
    }

    [Fact]
    public void CustomConversion_WhenValueObjectIsSerialized_ItIsSerializedAsItsPrimitive()
    {
        string json = JsonConvert.SerializeObject(CustomConvertedIntegerValueObject.Create(100));

        json.Should().Be("\"Always25\"");

        object? valueObjectDeserialized =
            JsonConvert.DeserializeObject<CustomConvertedIntegerValueObject>(json);

        valueObjectDeserialized.Should().NotBeNull();
        valueObjectDeserialized.Should().Be(CustomConvertedIntegerValueObject.Create(25));
    }
}