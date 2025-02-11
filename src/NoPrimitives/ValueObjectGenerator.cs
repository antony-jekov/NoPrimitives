using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.OutputGenerators;


namespace NoPrimitives;

[Generator]
public class ValueObjectGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<RecordDeclarationSyntax> provider = ValueObjectGenerator.CreateProvider(context);

        IncrementalValueProvider<(Compilation Left, ImmutableArray<RecordDeclarationSyntax> Right)> source =
            context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(source, ValueObjectGenerator.Execute);
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
            .Where(static typeDeclaration =>
                typeDeclaration is not null);

    private static void Execute(
        SourceProductionContext context,
        (Compilation Compilation, ImmutableArray<RecordDeclarationSyntax> RecordDeclarations) data)
    {
        var recordGenerator = new RecordGenerator();

        foreach (RecordDeclarationSyntax recordDeclaration in data.RecordDeclarations)
        {
            ValueObjectGenerator.ProcessDeclaration(context, recordDeclaration, data.Compilation, recordGenerator);
        }
    }

    private static void ProcessDeclaration(
        SourceProductionContext context,
        RecordDeclarationSyntax recordDeclaration,
        Compilation compilation,
        OutputGeneratorBase outputGenerator)
    {
        SemanticModel model = compilation.GetSemanticModel(recordDeclaration.SyntaxTree);

        if (model.GetDeclaredSymbol(recordDeclaration) is not INamedTypeSymbol symbol)
        {
            return;
        }

        ITypeSymbol? typeSymbol = ValueObjectGenerator.ExtractTypeArgument(symbol);

        if (typeSymbol is null)
        {
            return;
        }

        outputGenerator.Generate(context, symbol, typeSymbol);
    }

    private static ITypeSymbol? ExtractTypeArgument(INamedTypeSymbol symbol) =>
        ValueObjectGenerator.ExtractGenericTypeArgument(symbol) ??
        ValueObjectGenerator.ExtractConstructorTypeArgument(symbol);

    private static ITypeSymbol? ExtractConstructorTypeArgument(INamedTypeSymbol symbol)
    {
        AttributeData? attributeData =
            symbol.GetAttributes().FirstOrDefault(ValueObjectGenerator.IsValueObjectAttribute);

        TypedConstant? typeArgument = attributeData?.ConstructorArguments
            .FirstOrDefault(arg => arg.Kind == TypedConstantKind.Type);

        return typeArgument?.Value as ITypeSymbol;
    }

    private static ITypeSymbol? ExtractGenericTypeArgument(INamedTypeSymbol symbol)
    {
        INamedTypeSymbol? attributeClass = symbol.GetAttributes()
            .FirstOrDefault(ValueObjectGenerator.IsValueObjectAttribute)
            ?.AttributeClass;

        return attributeClass?.TypeArguments.FirstOrDefault();
    }

    private static bool IsValueObjectAttribute(AttributeData ad) =>
        ad.AttributeClass?.ToString().StartsWith("NoPrimitives.ValueObject") ?? false;
}