using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using NoPrimitives.Core;
using NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;
using NoPrimitives.Generation.OutputGenerators.Records;
using NoPrimitives.Generation.Pipeline;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Processors;

internal static class RecordsProcessor
{
    internal static void Process(SourceProductionContext context, ImmutableArray<RenderItem> valueObjects)
    {
        var pipeline = new OutputGenerationPipeline(
            new RecordGenerator(),
            new TypeConverterGenerator()
        );

        pipeline.Execute(context, valueObjects);
    }

    private static Integrations ScanIntegrations(IAssemblySymbol assemblySymbol) =>
        RecordsProcessor.ReferencesAssembly(assemblySymbol, "Newtonsoft.Json")
            ? Integrations.NewtonsoftJson
            : Integrations.None;

    private static bool ReferencesAssembly(IAssemblySymbol assemblySymbol, string assemblyName) =>
        assemblySymbol.Modules.SelectMany(m => m.ReferencedAssemblySymbols)
            .Any(referencedAssembly => referencedAssembly.Name == assemblyName);
}