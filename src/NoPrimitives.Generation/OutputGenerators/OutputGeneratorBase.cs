using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;


namespace NoPrimitives.Generation.OutputGenerators;

internal abstract class OutputGeneratorBase(string? suffix = null)
{
    public void Generate(
        SourceProductionContext context,
        INamedTypeSymbol symbol,
        ITypeSymbol typeSymbol)
    {
        string filename = OutputGeneratorBase.FilenameFor(symbol, suffix);
        string source = this.Render(symbol, typeSymbol);
        SourceText sourceText = SourceText.From(source, Encoding.UTF8);

        context.AddSource(filename, sourceText);
    }

    private static string FilenameFor(INamedTypeSymbol symbol, string? suffix)
    {
        string namespaceOrEmpty = symbol.ContainingNamespace.IsGlobalNamespace
            ? string.Empty
            : $".{symbol.ContainingNamespace.ToDisplayString()}";

        string suffixOrEmpty = suffix is not null
            ? $".{suffix}"
            : string.Empty;

        return $"NoPrimitives{namespaceOrEmpty}.{symbol.Name}{suffixOrEmpty}.g.cs";
    }

    private static string WriteNamespace(INamedTypeSymbol symbol) =>
        !symbol.ContainingNamespace.IsGlobalNamespace
            ? $"namespace {symbol.ContainingNamespace.ToDisplayString()}"
            : string.Empty;

    protected static string WrapInNamespaceFor(INamedTypeSymbol symbol, string source) =>
        symbol.ContainingNamespace.IsGlobalNamespace
            ? source
            : $$"""
                {{OutputGeneratorBase.WriteNamespace(symbol)}}
                {
                {{source}}
                }
                """;

    protected abstract string Render(INamedTypeSymbol symbol, ITypeSymbol typeSymbol);
}