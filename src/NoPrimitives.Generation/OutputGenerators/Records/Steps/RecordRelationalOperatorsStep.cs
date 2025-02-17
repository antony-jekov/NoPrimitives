using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordRelationalOperatorsStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string symbolName = context.Item.ValueObject.Name;

        if (!Util.IsNumericType(context.Item.Primitive) &&
            !Util.IsDateOrTimeType(context.Item.Primitive))
        {
            return;
        }

        string indentation = context.Indentation;

        builder.AppendLine($$"""

                             {{indentation}}public static bool operator > ({{symbolName}} left, {{symbolName}} right)
                             {{indentation}}{
                             {{indentation}}    return left.Value > right.Value;
                             {{indentation}}}

                             {{indentation}}public static bool operator < ({{symbolName}} left, {{symbolName}} right)
                             {{indentation}}{
                             {{indentation}}    return left.Value < right.Value;
                             {{indentation}}}
                                     
                             {{indentation}}public static bool operator >= ({{symbolName}} left, {{symbolName}} right)
                             {{indentation}}{
                             {{indentation}}    return left.Value >= right.Value;
                             {{indentation}}}

                             {{indentation}}public static bool operator <= ({{symbolName}} left, {{symbolName}} right)
                             {{indentation}}{
                             {{indentation}}    return left.Value <= right.Value;
                             {{indentation}}}
                             """);
    }
}