using System.Text.Json;


namespace NoPrimitives.Usage.Tests.ConversionTests.JsonConversions;

public class ConversionTests
{
    [Theory]
    [ClassData(typeof(ConvertTestData))]
    [ClassData(typeof(ConvertNullableTestData))]
    public void Conversion_ConvertsFromJsonToValueObject<T>(string json, T expectedVo)
    {
        var deserialized = JsonSerializer.Deserialize<Payload<T>>(json);

        deserialized.Should().NotBeNull();
        deserialized.SomeVo.Should().Be(expectedVo);
    }

    [Theory]
    [ClassData(typeof(ConvertTestData))]
    [ClassData(typeof(ConvertNullableTestData))]
    public void Conversion_ConvertsValueObjectToJson<T>(string expectedJson, T vo)
    {
        var payload = new Payload<T>(vo);
        string json = JsonSerializer.Serialize(payload);

        json.Should().Be(expectedJson);
    }

    public record Payload<T>(T SomeVo);
}