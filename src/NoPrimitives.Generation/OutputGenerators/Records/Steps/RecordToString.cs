using System.Text;
using Microsoft.CodeAnalysis;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordToString : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        bool isNullable = context.PrimitiveTypeSymbol.NullableAnnotation == NullableAnnotation.Annotated;
        string fallbackValue = isNullable ? " ?? string.Empty" : string.Empty;
        string conditionalAccessValue = isNullable ? "?" : string.Empty;

        builder.AppendLine($$"""

                             {{context.Indentation}}public override string ToString()
                             {{context.Indentation}}{
                             {{context.Indentation}}    return this.Value{{conditionalAccessValue}}.ToString(){{fallbackValue}};
                             {{context.Indentation}}}
                             """);
    }
}