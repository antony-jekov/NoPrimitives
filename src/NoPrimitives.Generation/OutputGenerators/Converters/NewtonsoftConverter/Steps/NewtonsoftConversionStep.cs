using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter.Steps;

internal class NewtonsoftConversionStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string indentation = context.Indentation;

        var src =
            $$"""
              {{indentation}}public override void WriteJson(JsonWriter writer, {{context.TypeName}} value, JsonSerializer serializer)
              {{indentation}}{
              {{indentation}}    writer.WriteValue(value?.Value);
              {{indentation}}}

              {{indentation}}public override {{context.TypeName}} ReadJson(JsonReader reader, Type objectType, {{context.TypeName}} existingValue,
              {{indentation}}    bool hasExistingValue, JsonSerializer serializer)
              {{indentation}}{
              {{indentation}}    return {{context.TypeName}}.Create(serializer.Deserialize<{{context.PrimitiveTypeName}}>(reader));
              {{indentation}}}
              """;

        builder.AppendLine(src);
    }
}