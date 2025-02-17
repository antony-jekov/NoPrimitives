using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Generation.Extensions;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Generation.OutputGenerators.Records;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.Processing;

internal static class DeclarationsProcessor
{
    internal static void Process(SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<TypeDeclarationSyntax> declarations)
    {
        Integrations? globalIntegrations = Util.ExtractGlobalIntegrations(compilation.Assembly);
        DeclarationsProcessor.ProcessRecords(context, compilation, declarations, globalIntegrations);
    }

    private static ImmutableArray<RenderItem> ProcessRecords(SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<TypeDeclarationSyntax> declarations,
        Integrations? globalIntegrations)
    {
        ImmutableArray<RenderItem> renderItems =
            declarations.OfType<RecordDeclarationSyntax>()
                .ToRenderItems(compilation, globalIntegrations);

        foreach (RenderItem item in renderItems)
        {
            IntegrationPipelines
                .GetFor(item.Integrations, DeclarationsProcessor.ProvideIntegratedRecordsGenerator)
                .Execute(context, item);
        }

        return renderItems;
    }

    private static OutputGeneratorBase ProvideIntegratedRecordsGenerator(
        ImmutableArray<AttributeStep> integrationsAttributes,
        ImmutableArray<IRenderStep> integrationsSteps) =>
        new RecordGenerator(integrationsAttributes, integrationsSteps);
}