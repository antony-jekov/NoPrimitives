using Newtonsoft.Json;


namespace NoPrimitives.NewtonsoftJson.Tests;

[ValueObject<int>]
[JsonConverter(typeof(CustomJsonConverter))]
internal partial record CustomConvertedIntegerValueObject;

internal class CustomJsonConverter : JsonConverter<CustomConvertedIntegerValueObject>
{
    public override void WriteJson(JsonWriter writer, CustomConvertedIntegerValueObject? value,
        JsonSerializer serializer)
    {
        writer.WriteValue("Always25");
    }

    public override CustomConvertedIntegerValueObject? ReadJson(JsonReader reader, Type objectType,
        CustomConvertedIntegerValueObject? existingValue, bool hasExistingValue, JsonSerializer serializer) =>
        CustomConvertedIntegerValueObject.Create(25);
}