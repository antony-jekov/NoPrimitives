using Microsoft.CodeAnalysis;
using NoPrimitives.Core;


namespace NoPrimitives.Rendering;

public record RenderItem
{
    public RenderItem(
        INamedTypeSymbol valueObject,
        ITypeSymbol primitive,
        Integrations integrations = Integrations.Default)
    {
        this.ValueObject = valueObject;
        this.Primitive = primitive;
        this.Integrations = integrations;
    }

    public INamedTypeSymbol ValueObject { get; }
    public ITypeSymbol Primitive { get; }

    public Integrations Integrations { get; }
}