using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Generation.Extensions;
using NoPrimitives.Generation.Processors;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation;

[Generator]
public class ValueObjectGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<RecordDeclarationSyntax> provider = ValueObjectGenerator.CreateProvider(context);

        IncrementalValueProvider<(Compilation Left, ImmutableArray<RecordDeclarationSyntax> Right)> source =
            context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(source, ValueObjectGenerator.GenerateRecordValueObjects);
    }

    private static IncrementalValuesProvider<RecordDeclarationSyntax> CreateProvider(
        IncrementalGeneratorInitializationContext context) =>
        context.SyntaxProvider.CreateSyntaxProvider(
                static (node, _) =>
                    node is RecordDeclarationSyntax
                    {
                        AttributeLists.Count: > 0,
                    },
                static (ctx, _) =>
                    (RecordDeclarationSyntax)ctx.Node
            )
            .Where(static typeDeclaration => typeDeclaration is not null);

    private static void GenerateRecordValueObjects(
        SourceProductionContext context,
        (Compilation Compilation, ImmutableArray<RecordDeclarationSyntax> TypeDeclarations) data)
    {
        ImmutableArray<RenderItem> recordSymbols =
            data.TypeDeclarations.ToRenderItems(data.Compilation);

        RecordsProcessor.Process(context, recordSymbols);
    }
}