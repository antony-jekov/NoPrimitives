using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.OutputGenerators;
using NoPrimitives.OutputGenerators.Converters.TypeConverter;
using NoPrimitives.OutputGenerators.Records;


namespace NoPrimitives;

[Generator]
public class ValueObjectGenerator : IIncrementalGenerator
{
    private static readonly ImmutableArray<OutputGeneratorBase> RecordGenerators =
    [
        new RecordGenerator(), new TypeConverterGenerator(),
    ];

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
            .Where(static typeDeclaration => typeDeclaration is not null);

    private static void Execute(
        SourceProductionContext context,
        (Compilation Compilation, ImmutableArray<RecordDeclarationSyntax> TypeDeclarations) data)
    {
        foreach (RecordDeclarationSyntax declarationSyntax in data.TypeDeclarations)
        {
            ValueObjectGenerator.ProcessTypeDeclaration(
                context,
                declarationSyntax,
                data.Compilation,
                ValueObjectGenerator.RecordGenerators
            );
        }
    }

    private static void ProcessTypeDeclaration(
        SourceProductionContext context,
        TypeDeclarationSyntax declarationSyntax,
        Compilation compilation,
        ImmutableArray<OutputGeneratorBase> generators)
    {
        (INamedTypeSymbol symbol, ITypeSymbol typeSymbol)? symbols =
            ValueObjectGenerator.SymbolsFor(compilation, declarationSyntax);

        if (symbols is null)
        {
            return;
        }

        (INamedTypeSymbol symbol, ITypeSymbol typeSymbol) = symbols.Value;

        foreach (OutputGeneratorBase outputGenerator in generators)
        {
            outputGenerator.Generate(context, symbol, typeSymbol);
        }
    }

    private static (INamedTypeSymbol symbol, ITypeSymbol typeSymbol)? SymbolsFor(Compilation compilation,
        TypeDeclarationSyntax declarationSyntax)
    {
        SemanticModel model = compilation.GetSemanticModel(declarationSyntax.SyntaxTree);

        if (model.GetDeclaredSymbol(declarationSyntax) is not INamedTypeSymbol symbol)
        {
            return null;
        }

        ITypeSymbol? typeSymbol = ValueObjectGenerator.ExtractTypeArgument(symbol);

        if (typeSymbol is null)
        {
            return null;
        }

        return (symbol, typeSymbol);
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