using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordRelationalOperatorsStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string symbolName = context.ValueObjectSymbol.Name;

        if (!Util.IsNumericType(context.PrimitiveTypeSymbol) &&
            !Util.IsDateOrTimeType(context.PrimitiveTypeSymbol))
        {
            return;
        }

        builder.AppendLine($$"""

                             {{context.Indentation}}public static bool operator > ({{symbolName}} left, {{symbolName}} right)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return left.Value > right.Value;
                             {{context.Indentation}}}

                             {{context.Indentation}}public static bool operator < ({{symbolName}} left, {{symbolName}} right)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return left.Value < right.Value;
                             {{context.Indentation}}}
                                     
                             {{context.Indentation}}public static bool operator >= ({{symbolName}} left, {{symbolName}} right)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return left.Value >= right.Value;
                             {{context.Indentation}}}

                             {{context.Indentation}}public static bool operator <= ({{symbolName}} left, {{symbolName}} right)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return left.Value <= right.Value;
                             {{context.Indentation}}}
                             """);
    }
}