using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter.Steps;

internal class NewtonsoftConverterAttributeStep : AttributeStep
{
    protected override string RenderAttribute(RenderContext context) =>
        $"Newtonsoft.Json.JsonConverter(typeof({context.TypeName}NewtonsoftJsonConverter))";
}