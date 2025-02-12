﻿using System.Text;
using Microsoft.CodeAnalysis;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;
using NSubstitute;


namespace NoPrimitives.Tests.RenderPipeline;

public class PipelineTests
{
    [Fact]
    public void Construct_WhenProvidedWithNoSteps_DoesNothing()
    {
        var pipeline = new Pipeline();

        var symbol = Substitute.For<INamedTypeSymbol>();
        var typeSymbol = Substitute.For<ITypeSymbol>();
        typeSymbol.ToDisplayString().Returns(string.Empty);

        var context = new RenderContext(symbol, typeSymbol);

        string result = pipeline.Execute(context);

        result.Should().BeEmpty();
    }

    [Fact]
    public void Execute_WhenGivenSteps_ExecutesInOrder()
    {
        var pipeline = new Pipeline(
            new ActionStep((context, builder, next) =>
            {
                var sb = new StringBuilder();
                next.Render(context, sb);

                builder.Append("{ ");
                builder.Append(sb);
                builder.Append(" }");
            }),
            new ActionStep((context, builder, next) =>
            {
                builder.Append("Step 1");
                next.Render(context, builder);
            }),
            new ActionStep((context, builder, next) =>
            {
                builder.Append(" -> Step 2");
                next.Render(context, builder);
            }),
            new ActionStep((context, builder, next) =>
            {
                builder.Append(" -> Step 3");
                next.Render(context, builder);
            })
        );

        var symbol = Substitute.For<INamedTypeSymbol>();
        var typeSymbol = Substitute.For<ITypeSymbol>();
        typeSymbol.ToDisplayString().Returns(string.Empty);

        var context = new RenderContext(symbol, typeSymbol);

        string result = pipeline.Execute(context);

        result.Should().Be("{ Step 1 -> Step 2 -> Step 3 }");
    }

    private class ActionStep(
        Action<RenderContext, StringBuilder, INextRenderStep> renderAction)
        : IRenderStep
    {
        public void Render(RenderContext context, StringBuilder builder, INextRenderStep next)
        {
            renderAction(context, builder, next);
        }
    }
}