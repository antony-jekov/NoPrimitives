using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Generation.Extensions;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Generation.OutputGenerators.Records;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.Processing;

internal class DeclarationsProcessor(Compilation compilation)
{
    private readonly Integrations? _globalIntegrations = Util.ExtractGlobalIntegrations(compilation.Assembly);

    internal void Process(SourceProductionContext context,
        ImmutableArray<TypeDeclarationSyntax> declarations)
    {
        ImmutableArray<RenderItem> recordItems =
            this.ProcessRecords<RecordDeclarationSyntax>(
                context,
                declarations,
                DeclarationsProcessor.ProvideIntegratedRecordsGenerator
            );
    }

    private ImmutableArray<RenderItem> ProcessRecords<TDeclaration>(SourceProductionContext context,
        ImmutableArray<TypeDeclarationSyntax> declarations,
        Func<ImmutableArray<AttributeStep>, ImmutableArray<IRenderStep>, OutputGeneratorBase> provider)
        where TDeclaration : TypeDeclarationSyntax
    {
        ImmutableArray<RenderItem> renderItems =
            declarations.OfType<TDeclaration>()
                .ToRenderItems(compilation, this._globalIntegrations);

        foreach (RenderItem item in renderItems)
        {
            IntegrationPipelines
                .GetFor(item.Integrations, provider)
                .Execute(context, item);
        }

        return renderItems;
    }

    private static RecordGenerator ProvideIntegratedRecordsGenerator(
        ImmutableArray<AttributeStep> integrationsAttributes, ImmutableArray<IRenderStep> integrationsSteps) =>
        new(integrationsAttributes, integrationsSteps);
}