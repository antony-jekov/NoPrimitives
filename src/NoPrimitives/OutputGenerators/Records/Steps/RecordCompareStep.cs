using System.Text;
using Microsoft.CodeAnalysis;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;


namespace NoPrimitives.OutputGenerators.Records.Steps;

internal class RecordCompareStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        bool isNullable = context.PrimitiveTypeSymbol.NullableAnnotation == NullableAnnotation.Annotated;
        string fallbackValue = isNullable ? " ?? -1" : string.Empty;
        string conditionalAccessValue = isNullable ? "?" : string.Empty;

        builder.AppendLine($$"""

                             {{context.Indentation}}public int CompareTo({{context.ValueObjectSymbol.Name}} other)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return this.Value{{conditionalAccessValue}}.CompareTo(other.Value){{fallbackValue}};
                             {{context.Indentation}}}

                             {{context.Indentation}}public int CompareTo(object other)
                             {{context.Indentation}}{
                             {{context.Indentation}}    if (other is null) return 1;
                             {{context.Indentation}}    if (other is not {{context.ValueObjectSymbol.Name}} vo) return 0;

                             {{context.Indentation}}    return CompareTo(vo);
                             {{context.Indentation}}}
                             """);
    }
}