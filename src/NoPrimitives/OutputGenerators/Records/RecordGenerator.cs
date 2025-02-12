using Microsoft.CodeAnalysis;
using NoPrimitives.OutputGenerators.Records.Steps;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;


namespace NoPrimitives.OutputGenerators.Records;

internal sealed class RecordGenerator : OutputGeneratorBase
{
    private static readonly Pipeline RenderPipeline = new(
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
        new RecordToString()
    );

    protected override string Render(INamedTypeSymbol symbol, ITypeSymbol typeSymbol) =>
        RecordGenerator.RenderPipeline.Execute(new RenderContext(symbol, typeSymbol));
}