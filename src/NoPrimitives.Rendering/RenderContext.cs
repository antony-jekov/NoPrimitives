namespace NoPrimitives.Rendering;

public record RenderContext
{
    private const string IndentationStep = "    ";

    public RenderContext(RenderItem item)
    {
        this.Item = item;
        this.TypeName = item.ValueObject.Name;
        this.PrimitiveTypeName = item.Primitive.ToDisplayString();
    }

    public RenderItem Item { get; }

    public string TypeName { get; }
    public string PrimitiveTypeName { get; }

    public string Indentation { get; private set; } = string.Empty;

    public RenderContext Indent() =>
        this with { Indentation = $"{this.Indentation}{RenderContext.IndentationStep}" };
}