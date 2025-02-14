using System.Text;
using Microsoft.CodeAnalysis;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordToString : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        bool isNullable = context.Item.Primitive.NullableAnnotation == NullableAnnotation.Annotated;
        string fallbackValue = isNullable ? " ?? string.Empty" : string.Empty;
        string conditionalAccessValue = isNullable ? "?" : string.Empty;
        string indentation = context.Indentation;

        builder.AppendLine($$"""

                             {{indentation}}public override string ToString()
                             {{indentation}}{
                             {{indentation}}    return this.Value{{conditionalAccessValue}}.ToString(){{fallbackValue}};
                             {{indentation}}}
                             """);
    }
}