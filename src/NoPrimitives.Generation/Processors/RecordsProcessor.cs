using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using NoPrimitives.Generation.Pipeline;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Processors;

internal static class RecordsProcessor
{
    internal static void Process(SourceProductionContext context, ImmutableArray<RenderItem> renderItems)
    {
        foreach (RenderItem item in renderItems)
        {
            OutputGenerationPipeline pipeline = Integrations.GetPipelineFor(item.Integrations);

            pipeline.Execute(context, item);
        }
    }
}