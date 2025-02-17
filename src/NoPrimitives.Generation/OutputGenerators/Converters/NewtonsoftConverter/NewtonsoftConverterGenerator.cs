using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter.Steps;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter;

internal class NewtonsoftConverterGenerator()
    : OutputGeneratorBase("NewtonsoftJsonConverter")
{
    private static readonly RenderPipeline Pipeline = new(
        new NamespaceStep(),
        new UsingsStep("System", "Newtonsoft.Json"),
        new NewtonsoftConverterDeclarationStep(),
        new NewtonsoftConversionStep()
    );

    protected override string Render(RenderItem item)
    {
        var context = new RenderContext(item);
        return NewtonsoftConverterGenerator.Pipeline.Execute(context);
    }
}