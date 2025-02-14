using NoPrimitives.Core;
using NoPrimitives.Generation.OutputGenerators.Records.Steps;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records;

internal sealed class RecordGenerator : OutputGeneratorBase
{
    private readonly Rendering.Pipeline _renderPipeline = new(
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
        this._renderPipeline.Execute(new RenderContext(item));
}