using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;


namespace NoPrimitives.OutputGenerators;

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

    protected static string AccessModifierFor(INamedTypeSymbol symbol) =>
        symbol.DeclaredAccessibility == Accessibility.Public ? "public" : "internal";

    protected static bool IsNumericType(ITypeSymbol typeSymbol)
    {
        typeSymbol = OutputGeneratorBase.ExtractTypeFromNullableType(typeSymbol);

        return typeSymbol.SpecialType is SpecialType.System_Byte or SpecialType.System_SByte
                   or SpecialType.System_Int16 or SpecialType.System_UInt16
                   or SpecialType.System_Int32 or SpecialType.System_UInt32
                   or SpecialType.System_Int64 or SpecialType.System_UInt64
                   or SpecialType.System_Decimal or SpecialType.System_Double
                   or SpecialType.System_Single or SpecialType.System_Char
               || typeSymbol.AllInterfaces
                   .Any(i => i.ToDisplayString().StartsWith("System.Numerics.INumber<"));
    }

    protected static ITypeSymbol ExtractTypeFromNullableType(ITypeSymbol typeSymbol) =>
        typeSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T &&
        typeSymbol is INamedTypeSymbol { TypeArguments.Length: 1 } namedTypeSymbol
            ? namedTypeSymbol.TypeArguments[0]
            : typeSymbol;

    protected static string WrapInNamespaceFor(INamedTypeSymbol symbol, string source) =>
        symbol.ContainingNamespace.IsGlobalNamespace
            ? source
            : $$"""
                {{OutputGeneratorBase.WriteNamespace(symbol)}}
                {
                {{source}}
                }
                """;

    protected static bool IsDateOrTimeType(ITypeSymbol typeSymbol)
    {
        typeSymbol = OutputGeneratorBase.ExtractTypeFromNullableType(typeSymbol);

        return (typeSymbol.ContainingNamespace?.ToDisplayString() == "System" &&
                typeSymbol.Name is "DateTime" or "TimeSpan" or "DateOnly" or "TimeOnly" or "DateTimeOffset")
               || typeSymbol.BaseType?.ToDisplayString() == "System.DateTime";
    }

    protected static bool AlreadyHasAttributeStartingWith(INamedTypeSymbol symbol, string prefix) =>
        symbol.GetAttributes()
            .Any(a => a.AttributeClass?.ToString().StartsWith(prefix) ?? false);

    protected abstract string Render(INamedTypeSymbol symbol, ITypeSymbol typeSymbol);
}