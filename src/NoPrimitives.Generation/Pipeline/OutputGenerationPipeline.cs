using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Pipeline;

internal class OutputGenerationPipeline(params OutputGeneratorBase[] generators)
{
    private readonly ImmutableArray<OutputGeneratorBase> _outputGenerators = [..generators];

    public void Execute(SourceProductionContext context, ImmutableArray<RenderItem> renderItems)
    {
        foreach (RenderItem item in renderItems)
        {
            foreach (OutputGeneratorBase generator in this._outputGenerators)
            {
                generator.Generate(context, item);
            }
        }
    }
}