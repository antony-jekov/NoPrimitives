using System.Text;
using Microsoft.CodeAnalysis;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordCompareStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        bool isNullable = context.Item.Primitive.NullableAnnotation == NullableAnnotation.Annotated;
        string fallbackValue = isNullable ? " ?? -1" : string.Empty;
        string compareValue = isNullable ? ".Value" : string.Empty;
        string conditionalAccessValue = isNullable ? "?" : string.Empty;
        string indentation = context.Indentation;

        builder.AppendLine($$"""

                             {{indentation}}public int CompareTo({{context.Item.ValueObject.Name}} other)
                             {{indentation}}{
                             {{indentation}}    return this.Value{{conditionalAccessValue}}.CompareTo(other.Value{{compareValue}}){{fallbackValue}};
                             {{indentation}}}

                             {{indentation}}public int CompareTo(object other)
                             {{indentation}}{
                             {{indentation}}    if (other is null) return 1;
                             {{indentation}}    if (other is not {{context.Item.ValueObject.Name}} vo) return 0;

                             {{indentation}}    return CompareTo(vo);
                             {{indentation}}}
                             """);
    }
}