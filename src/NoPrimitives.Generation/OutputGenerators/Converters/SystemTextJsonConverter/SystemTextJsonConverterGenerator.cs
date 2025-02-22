using NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter.Steps;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter;

internal class SystemTextJsonConverterGenerator() : OutputGeneratorBase("SystemTextJsonConverter")
{
    private static readonly RenderPipeline Pipeline = new(
        new NamespaceStep(),
        new UsingsStep("System", "System.Text.Json", "System.Text.Json.Serialization"),
        new SystemTextJsonDeclarationStep(),
        new SystemTextJsonSerializeStep(),
        new SystemTextJsonDeserializeStep()
    );

    protected override string Render(RenderItem item) =>
        SystemTextJsonConverterGenerator.Pipeline.Execute(new RenderContext(item));
}