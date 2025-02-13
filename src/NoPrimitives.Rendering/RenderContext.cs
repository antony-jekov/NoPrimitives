using Microsoft.CodeAnalysis;


namespace NoPrimitives.Rendering;

public record RenderContext(INamedTypeSymbol ValueObjectSymbol, ITypeSymbol PrimitiveTypeSymbol)
{
    private const string IndentationStep = "    ";

    public INamedTypeSymbol ValueObjectSymbol { get; } = ValueObjectSymbol;

    public ITypeSymbol PrimitiveTypeSymbol { get; } = PrimitiveTypeSymbol;

    public string PrimitiveTypeName { get; } = PrimitiveTypeSymbol.ToDisplayString();

    public string Indentation { get; private set; } = string.Empty;

    public RenderContext Indent() =>
        this with { Indentation = $"{this.Indentation}{RenderContext.IndentationStep}" };
}