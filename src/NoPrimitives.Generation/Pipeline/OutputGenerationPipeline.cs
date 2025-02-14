using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Pipeline;

internal class OutputGenerationPipeline(params OutputGeneratorBase[] generators)
{
    private readonly ImmutableArray<OutputGeneratorBase> _outputGenerators = [..generators];

    public void Execute(SourceProductionContext context, ImmutableArray<RenderItem> valueObjects)
    {
        foreach (RenderItem valueObject in valueObjects)
        {
            OutputGenerationPipeline.ProcessValueObject(context, valueObject, this._outputGenerators);
        }
    }

    private static void ProcessValueObject(SourceProductionContext context, RenderItem render,
        ImmutableArray<OutputGeneratorBase> generators)
    {
        foreach (OutputGeneratorBase generator in generators)
        {
            generator.Generate(context, render);
        }
    }
}