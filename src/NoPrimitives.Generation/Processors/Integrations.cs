using System.Collections.Generic;
using System.Collections.Immutable;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter.Steps;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;
using NoPrimitives.Generation.OutputGenerators.Records;
using NoPrimitives.Generation.Pipeline;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.Processors;

internal abstract class Integrations
{
    private static readonly Dictionary<NoPrimitives.Integrations, OutputGenerationPipeline> Pipelines = new();

    internal static OutputGenerationPipeline GetPipelineFor(NoPrimitives.Integrations integration)
    {
        if (Integrations.Pipelines.TryGetValue(integration, out OutputGenerationPipeline pipeline))
        {
            return pipeline;
        }

        pipeline = Integrations.BuildPipelineFor(integration);

        Integrations.Pipelines.Add(integration, pipeline);
        return pipeline;
    }

    private static OutputGenerationPipeline BuildPipelineFor(NoPrimitives.Integrations integration)
    {
        var generators = new List<OutputGeneratorBase>
        {
            new RecordGenerator(Integrations.BuildAttributeStepsFor(integration)),
        };

        if (integration.HasFlag(NoPrimitives.Integrations.TypeConversions))
        {
            generators.Add(new TypeConverterGenerator());
        }

        if (integration.HasFlag(NoPrimitives.Integrations.NewtonsoftJson))
        {
            generators.Add(new NewtonsoftConverterGenerator());
        }

        return new OutputGenerationPipeline(generators.ToImmutableArray());
    }

    private static AttributeStep[] BuildAttributeStepsFor(NoPrimitives.Integrations integration)
    {
        var attributes = new List<AttributeStep>();

        if (integration.HasFlag(NoPrimitives.Integrations.TypeConversions))
        {
            attributes.Add(new TypeConverterAttributeStep());
        }

        if (integration.HasFlag(NoPrimitives.Integrations.NewtonsoftJson))
        {
            attributes.Add(new NewtonsoftConverterAttributeStep());
        }

        return [.. attributes];
    }
}