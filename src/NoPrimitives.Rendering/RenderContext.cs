using NoPrimitives.Core;


namespace NoPrimitives.Rendering;

public record RenderContext
{
    private const string IndentationStep = "    ";

    public RenderContext(RenderItem render)
    {
        this.Item = render;
        this.PrimitiveTypeName = render.Primitive.ToDisplayString();
    }

    public RenderItem Item { get; }

    public string PrimitiveTypeName { get; }

    public string Indentation { get; private set; } = string.Empty;

    public RenderContext Indent() =>
        this with { Indentation = $"{this.Indentation}{RenderContext.IndentationStep}" };
}