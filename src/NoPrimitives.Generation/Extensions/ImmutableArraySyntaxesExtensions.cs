using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Core;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Extensions;

internal static class ImmutableArraySyntaxesExtensions
{
    public static ImmutableArray<RenderItem> ToRenderItems(
        this ImmutableArray<RecordDeclarationSyntax> recordDeclarations, Compilation compilation)
    {
        Integrations? globalIntegrations = Util.ExtractIntegrations(compilation.Assembly);
        return
        [
            ..recordDeclarations
                .Select(d => ImmutableArraySyntaxesExtensions.RenderItemFor(compilation, d, globalIntegrations))
                .Where(s => s != null),
        ];
    }

    private static RenderItem? RenderItemFor(Compilation compilation,
        TypeDeclarationSyntax declarationSyntax, Integrations? globalIntegrations)
    {
        SemanticModel model = compilation.GetSemanticModel(declarationSyntax.SyntaxTree);

        if (model.GetDeclaredSymbol(declarationSyntax) is not INamedTypeSymbol symbol)
        {
            return null;
        }

        ITypeSymbol? typeSymbol = ImmutableArraySyntaxesExtensions.ExtractTypeArgument(symbol);
        Integrations valueObjectIntegrations =
            Util.ExtractIntegrations(symbol) ?? globalIntegrations ?? Integrations.Default;

        return typeSymbol is not null ? new RenderItem(symbol, typeSymbol, valueObjectIntegrations) : null;
    }

    private static ITypeSymbol? ExtractTypeArgument(INamedTypeSymbol symbol) =>
        ImmutableArraySyntaxesExtensions.ExtractGenericTypeArgument(symbol) ??
        ImmutableArraySyntaxesExtensions.ExtractConstructorTypeArgument(symbol);

    private static ITypeSymbol? ExtractConstructorTypeArgument(INamedTypeSymbol symbol)
    {
        AttributeData? attributeData =
            symbol.GetAttributes().FirstOrDefault(Util.IsValueObjectAttribute);

        TypedConstant? typeArgument = attributeData?.ConstructorArguments
            .FirstOrDefault(arg => arg.Kind == TypedConstantKind.Type);

        return typeArgument?.Value as ITypeSymbol;
    }

    private static ITypeSymbol? ExtractGenericTypeArgument(INamedTypeSymbol symbol)
    {
        INamedTypeSymbol? attributeClass = symbol.GetAttributes()
            .FirstOrDefault(Util.IsValueObjectAttribute)
            ?.AttributeClass;

        return attributeClass?.TypeArguments.FirstOrDefault();
    }
}