using System.Text;


namespace NoPrimitives.Rendering.Steps;

public interface IRenderStep
{
    void Render(RenderContext context, StringBuilder builder, INextRenderStep next);
}