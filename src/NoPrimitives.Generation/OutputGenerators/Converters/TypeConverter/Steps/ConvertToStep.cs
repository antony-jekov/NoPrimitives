using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;

internal class ConvertToStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string primitiveType = Util.ExtractTypeFromNullableType(context.Item.Primitive).ToDisplayString();
        string indentation = context.Indentation;

        var src =
            $$"""

              {{indentation}}public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
              {{indentation}}{
              {{indentation}}    return sourceType == typeof({{primitiveType}}) || sourceType == typeof(string);
              {{indentation}}}

              {{indentation}}public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
              {{indentation}}{
              {{indentation}}    if (value is not {{context.TypeName}} vo) {
              {{indentation}}        throw new NotSupportedException();
              {{indentation}}    }

              {{indentation}}    if (destinationType == typeof(string))
              {{indentation}}    {
              {{indentation}}         return vo.ToString();
              {{indentation}}    }

              {{indentation}}    if (destinationType == typeof({{primitiveType}}))
              {{indentation}}    {
              {{indentation}}        return vo.Value;
              {{indentation}}    }

              {{indentation}}    throw new NotSupportedException();
              {{indentation}}}
              """;

        builder.AppendLine(src);
    }
}