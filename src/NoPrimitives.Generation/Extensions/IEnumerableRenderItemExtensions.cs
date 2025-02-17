using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Generation.OutputGenerators;
using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.Extensions;

internal static class EnumerableRenderItemExtensions
{
    public static ImmutableArray<RenderItem> ToRenderItems(
        this IEnumerable<TypeDeclarationSyntax> declarations, Compilation compilation,
        Integrations? globalIntegrations) =>
    [
        ..declarations
            .Select(declaration =>
                EnumerableRenderItemExtensions.RenderItemFor(compilation, declaration, globalIntegrations)
            ).OfType<RenderItem>(),
    ];

    private static RenderItem? RenderItemFor(Compilation compilation,
        TypeDeclarationSyntax declarationSyntax, Integrations? globalIntegrations)
    {
        SemanticModel model = compilation.GetSemanticModel(declarationSyntax.SyntaxTree);

        if (model.GetDeclaredSymbol(declarationSyntax) is not INamedTypeSymbol symbol)
        {
            return null;
        }

        Integrations integrations = Util.ExtractValueObjectIntegrations(symbol, globalIntegrations);
        ITypeSymbol? typeSymbol = EnumerableRenderItemExtensions.ExtractTypeArgument(symbol);

        return typeSymbol is not null ? new RenderItem(symbol, typeSymbol, integrations) : null;
    }

    private static ITypeSymbol? ExtractTypeArgument(INamedTypeSymbol symbol) =>
        EnumerableRenderItemExtensions.ExtractGenericTypeArgument(symbol) ??
        EnumerableRenderItemExtensions.ExtractConstructorTypeArgument(symbol);

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