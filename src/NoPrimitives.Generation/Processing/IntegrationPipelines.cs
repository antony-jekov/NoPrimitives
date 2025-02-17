using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter.Steps;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;
using NoPrimitives.Generation.Pipeline;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.Processing;

internal static class IntegrationPipelines
{
    private static readonly Dictionary<Integrations, OutputGenerationPipeline> Pipelines = new();

    internal static OutputGenerationPipeline GetFor(Integrations integration,
        Func<ImmutableArray<AttributeStep>, ImmutableArray<IRenderStep>, OutputGeneratorBase>
            valueObjectGeneratorProvider)
    {
        if (IntegrationPipelines.Pipelines.TryGetValue(integration, out OutputGenerationPipeline pipeline))
        {
            return pipeline;
        }

        pipeline = IntegrationPipelines.BuildPipelineFor(integration, valueObjectGeneratorProvider);

        IntegrationPipelines.Pipelines.Add(integration, pipeline);
        return pipeline;
    }

    private static OutputGenerationPipeline BuildPipelineFor(Integrations integration,
        Func<ImmutableArray<AttributeStep>, ImmutableArray<IRenderStep>, OutputGeneratorBase>
            valueObjectGeneratorProvider)
    {
        var generators = new List<OutputGeneratorBase>
        {
            valueObjectGeneratorProvider(IntegrationPipelines.BuildAttributeStepsFor(integration), []),
        };

        if (integration.HasFlag(Integrations.TypeConversions))
        {
            generators.Add(new TypeConverterGenerator());
        }

        if (integration.HasFlag(Integrations.NewtonsoftJson))
        {
            generators.Add(new NewtonsoftConverterGenerator());
        }

        return new OutputGenerationPipeline(generators.ToImmutableArray());
    }

    private static ImmutableArray<AttributeStep> BuildAttributeStepsFor(Integrations integration)
    {
        var attributes = new List<AttributeStep>();

        if (integration.HasFlag(Integrations.TypeConversions))
        {
            attributes.Add(new TypeConverterAttributeStep());
        }

        if (integration.HasFlag(Integrations.NewtonsoftJson))
        {
            attributes.Add(new NewtonsoftConverterAttributeStep());
        }

        return [.. attributes];
    }
}