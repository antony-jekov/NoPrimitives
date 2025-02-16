using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;
using NoPrimitives.Generation.OutputGenerators.Records;
using NoPrimitives.Generation.Pipeline;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Processors;

internal static class RecordsProcessor
{
    internal static void Process(SourceProductionContext context, ImmutableArray<RenderItem> renderItems)
    {
        var pipeline = new OutputGenerationPipeline(
            new RecordGenerator(),
            new TypeConverterGenerator(),
            new NewtonsoftConverterGenerator()
        );

        pipeline.Execute(context, renderItems);
    }
}