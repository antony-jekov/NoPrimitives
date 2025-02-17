using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;


namespace NoPrimitives.Rendering.Steps;

public abstract class AttributeStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string attribute = this.RenderAttribute(context);
        AttributeStep.AddAttributeIfNotPresent(context, attribute, builder);
    }

    protected static void AddAttributeIfNotPresent(RenderContext context, string attribute, StringBuilder builder)
    {
        string attributeFullName = AttributeStep.ExtractAttributeFullName(attribute);

        if (AttributeStep.AlreadyHasAttributeStartingWith(context.Item.ValueObject, attributeFullName))
        {
            return;
        }

        builder.AppendLine($"{context.Indentation}[{attribute}]");
    }

    private static string ExtractAttributeFullName(string attribute)
    {
        int indexOf = attribute.IndexOf('(');
        return attribute.Substring(0, indexOf);
    }

    private static bool AlreadyHasAttributeStartingWith(INamedTypeSymbol symbol, string prefix) =>
        symbol.GetAttributes()
            .Any(a => a.AttributeClass?.ToString().StartsWith(prefix) ?? false);

    protected abstract string RenderAttribute(RenderContext context);
}