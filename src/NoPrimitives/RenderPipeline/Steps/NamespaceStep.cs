using System.Text;


namespace NoPrimitives.RenderPipeline.Steps;

internal class NamespaceStep : ScopeStartStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        if (context.ValueObjectSymbol.ContainingNamespace.IsGlobalNamespace)
        {
            return;
        }

        string namespaceName = context.ValueObjectSymbol.ContainingNamespace.ToDisplayString();

        builder.AppendLine($"{context.Indentation}namespace {namespaceName}");
    }
}