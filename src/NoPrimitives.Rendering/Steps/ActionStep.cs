using System;
using System.Text;


namespace NoPrimitives.Rendering.Steps;

public class ActionStep(Action<RenderContext, StringBuilder> action) : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        action(context, builder);
    }
}