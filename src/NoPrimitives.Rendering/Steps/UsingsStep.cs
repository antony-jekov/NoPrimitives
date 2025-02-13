using System.Text;


namespace NoPrimitives.Rendering.Steps;

public class UsingsStep(params string[] usings) : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        foreach (string use in usings)
        {
            builder.AppendLine($"{context.Indentation}using {use};");
        }
    }
}