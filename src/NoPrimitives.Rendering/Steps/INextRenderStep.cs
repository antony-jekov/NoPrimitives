using System.Text;


namespace NoPrimitives.Rendering.Steps;

public interface INextRenderStep
{
    void Render(RenderContext context, StringBuilder builder);
}