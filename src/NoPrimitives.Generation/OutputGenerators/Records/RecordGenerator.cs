using System.Collections.Immutable;
using NoPrimitives.Generation.OutputGenerators.Records.Steps;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records;

internal sealed class RecordGenerator(
    ImmutableArray<AttributeStep> integrationAttributes,
    ImmutableArray<IRenderStep> integrationSteps) : OutputGeneratorBase
{
    private readonly RenderPipeline _renderPipeline = new(
        [
            new NamespaceStep(),
            new UsingsStep(
                "System",
                "System.CodeDom.Compiler",
                "System.Diagnostics.CodeAnalysis",
                "NoPrimitives"
            ),
            new RecordAttributesStep(integrationAttributes),
            new RecordTypeDeclarationStep(),
            new RecordConstructorStep(),
            new RecordFactoryMethodStep(),
            new RecordImplicitOperatorsStep(),
            new RecordCompareStep(),
            new RecordRelationalOperatorsStep(),
            new RecordParsableStep(),
            new RecordToString(),
            ..integrationSteps,
        ]
    );

    protected override string Render(RenderItem item) =>
        this._renderPipeline.Execute(new RenderContext(item));
}