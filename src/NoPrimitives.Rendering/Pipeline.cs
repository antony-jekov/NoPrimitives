using System.Text;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Rendering;

public class Pipeline(params IRenderStep[] steps)
{
    private static readonly StepWrapper TerminationStep = new();

    private readonly StepWrapper _stepsWrapped = Pipeline.BuildChain(steps);

    public string Execute(RenderContext context)
    {
        var builder = new StringBuilder(steps.Length * 256);
        this._stepsWrapped.Render(context, builder);

        return builder.ToString();
    }

    private static StepWrapper BuildChain(IRenderStep[] steps)
    {
        if (steps.Length == 0)
        {
            return Pipeline.TerminationStep;
        }

        StepWrapper current = Pipeline.TerminationStep;

        for (int i = steps.Length - 1; i >= 0; i--)
        {
            IRenderStep step = steps[i];
            var next = new StepWrapper(step, current);

            current = next;
        }

        return current;
    }

    private class StepWrapper(IRenderStep? step = null, INextRenderStep? next = null) : INextRenderStep
    {
        public void Render(RenderContext context, StringBuilder builder)
        {
            if (next is null)
            {
                return;
            }

            step?.Render(context, builder, next);
        }
    }
}