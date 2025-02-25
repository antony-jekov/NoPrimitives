using Newtonsoft.Json;
using NoPrimitives.NewtonsoftJson.Tests.TestData;


namespace NoPrimitives.NewtonsoftJson.Tests;

public class SerializingConversionTests
{
    [Theory]
    [ClassData(typeof(ValueObjectsToJsonData))]
    [ClassData(typeof(NullableValueObjectsToJsonData))]
    public void Conversion_WhenValueObjectIsSerialized_ItIsSerializedAsItsPrimitive<T>(IValueObject<T> valueObject,
        string expectedJson, Type _)
    {
        string json = JsonConvert.SerializeObject(valueObject);
        json.Should().Be(expectedJson);
    }

    [Theory]
    [ClassData(typeof(ValueObjectsToJsonData))]
    [ClassData(typeof(NullableValueObjectsToJsonData))]
    public void Conversion_WhenPrimitiveValueIsDeserialized_ItDeserializesAsItsValueObject<T>(
        IValueObject<T> valueObject, string json, Type expectedType)
    {
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