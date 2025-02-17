using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Generation.Processing;


namespace NoPrimitives.Generation;

[Generator]
[SuppressMessage("ReSharper", "SuggestVarOrType_Elsewhere")]
public class ValueObjectGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var source = ValueObjectGenerator.CreateSource(context);
        context.RegisterSourceOutput(source, ValueObjectGenerator.Execute);
    }

    private static void Execute(SourceProductionContext context,
        (Compilation compilation, ImmutableArray<TypeDeclarationSyntax> declarations) args)
    {
        var declarationsProcessor = new DeclarationsProcessor(args.compilation);
        declarationsProcessor.Process(context, args.declarations);
    }

    private static
        IncrementalValueProvider<(Compilation compilation, ImmutableArray<TypeDeclarationSyntax> declarations)>
        CreateSource(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
                ValueObjectGenerator.Predicate,
                ValueObjectGenerator.Transform)
            .Where(static declaration => declaration is not null);

        return context.CompilationProvider.Combine(provider.Collect());
    }

    private static bool Predicate(SyntaxNode node, CancellationToken _) =>
        node is TypeDeclarationSyntax { AttributeLists.Count: > 0 };

    private static TypeDeclarationSyntax Transform(GeneratorSyntaxContext ctx, CancellationToken _) =>
        (TypeDeclarationSyntax)ctx.Node;
}