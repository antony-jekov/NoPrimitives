using System;
using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordAttributesStep(params AttributeStep[] integrationAttributes) : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        Version? assemblyVersion = References.Assembly.Value.GetName().Version;

        builder.Append("\n\n");
        builder.AppendLine($"""{context.Indentation}[GeneratedCode("NoPrimitives", "{assemblyVersion}")]""");

        builder.AppendLine($"""{context.Indentation}[ExcludeFromCodeCoverage(Justification = "Generated Code")]""");

        this.AddForIntegrations(context, builder);
    }

    private void AddForIntegrations(RenderContext context, StringBuilder builder)
    {
        foreach (AttributeStep attribute in integrationAttributes)
        {
            attribute.Render(context, builder, RenderPipeline.TerminationStep);
        }
    }
}