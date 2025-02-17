using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Pipeline;

internal class OutputGenerationPipeline(ImmutableArray<OutputGeneratorBase> generators)
{
    public OutputGenerationPipeline(params OutputGeneratorBase[] generators)
        : this(ImmutableArray.CreateRange(generators))
    {
    }

    public void Execute(SourceProductionContext context, RenderItem item)
    {
        foreach (OutputGeneratorBase generator in generators)
        {
            generator.Generate(context, item);
        }
    }
}