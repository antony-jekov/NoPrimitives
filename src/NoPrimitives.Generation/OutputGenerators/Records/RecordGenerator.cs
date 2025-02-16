using NoPrimitives.Generation.OutputGenerators.Records.Steps;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records;

internal sealed class RecordGenerator : OutputGeneratorBase
{
    private static readonly RenderPipeline RenderPipeline = new(
        new NamespaceStep(),
        new UsingsStep(
            "System",
            "System.CodeDom.Compiler",
            "System.Diagnostics.CodeAnalysis"
        ),
        new RecordAttributesStep(),
        new RecordTypeDeclarationStep(),
        new RecordConstructorStep(),
        new RecordFactoryMethodStep(),
        new RecordImplicitOperatorsStep(),
        new RecordCompareStep(),
        new RecordRelationalOperatorsStep(),
        new RecordParsableStep(),
        new RecordToString()
    );

    protected override string Render(RenderItem item) =>
        RecordGenerator.RenderPipeline.Execute(new RenderContext(item));
}