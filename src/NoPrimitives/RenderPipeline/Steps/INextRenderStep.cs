using System.Text;


namespace NoPrimitives.RenderPipeline.Steps;

internal interface INextRenderStep
{
    void Render(RenderContext context, StringBuilder builder);
}