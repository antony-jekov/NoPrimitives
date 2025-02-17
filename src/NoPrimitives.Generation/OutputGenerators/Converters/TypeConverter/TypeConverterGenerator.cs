using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;

internal class TypeConverterGenerator() : OutputGeneratorBase("TypeConverter")
{
    private readonly RenderPipeline _pipeline = new(
        new NamespaceStep(),
        new UsingsStep("System", "System.Globalization", "System.ComponentModel"),
        new TypeConverterDeclarationStep(),
        new ConvertFromStep(),
        new ConvertToStep()
    );

    protected override string Render(RenderItem item)
    {
        var context = new RenderContext(item);
        string src = this._pipeline.Execute(context);

        return src;
    }
}