using System.Text;


namespace NoPrimitives.RenderPipeline.Steps;

internal interface IRenderStep
{
    void Render(RenderContext context, StringBuilder builder, INextRenderStep next);
}