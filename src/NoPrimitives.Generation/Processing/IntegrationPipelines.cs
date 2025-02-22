using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter.Steps;
using NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter.Steps;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;
using NoPrimitives.Generation.Pipeline;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.Processing;

internal static class IntegrationPipelines
{
    private static readonly ConcurrentDictionary<Integrations, OutputGenerationPipeline> Pipelines = new();

    internal static OutputGenerationPipeline GetFor(Integrations integration,
        Func<ImmutableArray<AttributeStep>, ImmutableArray<IRenderStep>, OutputGeneratorBase>
            valueObjectGeneratorProvider)
    {
        if (IntegrationPipelines.Pipelines.TryGetValue(integration, out OutputGenerationPipeline pipeline))
        {
            return pipeline;
        }

        pipeline = IntegrationPipelines.BuildPipelineFor(integration, valueObjectGeneratorProvider);

        IntegrationPipelines.Pipelines.TryAdd(integration, pipeline);
        return pipeline;
    }

    private static OutputGenerationPipeline BuildPipelineFor(Integrations integration,
        Func<ImmutableArray<AttributeStep>, ImmutableArray<IRenderStep>, OutputGeneratorBase>
            valueObjectGeneratorProvider)
    {
        ImmutableArray<AttributeStep> attributeSteps = IntegrationPipelines.BuildAttributeStepsFor(integration);
        ImmutableArray<IRenderStep> integrationSteps = IntegrationPipelines.BuildIntegrationStepsFor(integration);
        var generators = new List<OutputGeneratorBase>
        {
            valueObjectGeneratorProvider(attributeSteps, integrationSteps),
        };

        if (integration.HasFlag(Integrations.TypeConversions))
        {
            generators.Add(new TypeConverterGenerator());
        }

        if (integration.HasFlag(Integrations.NewtonsoftJson))
        {
            generators.Add(new NewtonsoftConverterGenerator());
        }

        if (integration.HasFlag(Integrations.SystemTextJson))
        {
            generators.Add(new SystemTextJsonConverterGenerator());
        }

        return new OutputGenerationPipeline([..generators]);
    }

    private static ImmutableArray<IRenderStep> BuildIntegrationStepsFor(Integrations integration)
    {
        var integrationSteps = new List<IRenderStep>();

        return [..integrationSteps];
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

        if (integration.HasFlag(Integrations.SystemTextJson))
        {
            attributes.Add(new SystemTextJsonConverterAttributeStep());
        }

        return [.. attributes];
    }
}