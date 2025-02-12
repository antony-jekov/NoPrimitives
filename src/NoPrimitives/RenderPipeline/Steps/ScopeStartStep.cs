﻿using System.Text;


namespace NoPrimitives.RenderPipeline.Steps;

internal abstract class ScopeStartStep : IRenderStep
{
    public void Render(RenderContext context, StringBuilder builder, INextRenderStep next)
    {
        this.Render(context, builder);
        this.RenderScope(context, builder, next);
    }

    protected abstract void Render(RenderContext context, StringBuilder builder);

    private void RenderScope(RenderContext context, StringBuilder builder, INextRenderStep next)
    {
        var scopedBuilder = new StringBuilder();
        next.Render(context.Indent(), scopedBuilder);

        builder.AppendLine($$"""
                             {{context.Indentation}}{
                             {{context.Indentation}}{{scopedBuilder}}
                             {{context.Indentation}}}
                             """);
    }
}