using System.Text;


namespace NoPrimitives.Rendering.Steps;

public class NamespaceStep : ScopeStartStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        if (context.Item.ValueObject.ContainingNamespace.IsGlobalNamespace)
        {
            return;
        }

        string namespaceName = context.Item.ValueObject.ContainingNamespace.ToDisplayString();

        builder.AppendLine($"{context.Indentation}namespace {namespaceName}");
    }
}