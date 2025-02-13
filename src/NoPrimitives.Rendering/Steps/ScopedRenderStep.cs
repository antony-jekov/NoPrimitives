using System.Text;


namespace NoPrimitives.Rendering.Steps;

public abstract class ScopedRenderStep : IRenderStep
{
    public void Render(RenderContext context, StringBuilder builder, INextRenderStep next)
    {
        this.Render(context, builder);
        next.Render(context, builder);
    }

    protected abstract void Render(RenderContext context, StringBuilder builder);
}