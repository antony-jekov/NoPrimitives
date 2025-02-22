using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter.Steps;

internal class SystemTextJsonConverterAttributeStep : AttributeStep
{
    protected override string RenderAttribute(RenderContext context) =>
        $"System.Text.Json.Serialization.JsonConverter(typeof({context.TypeName}SystemTextJsonConverter))";
}