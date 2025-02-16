using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;

internal class ConvertFromStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string indentation = context.Indentation;
        string primitiveType = Util.ExtractTypeFromNullableType(context.Item.Primitive).ToDisplayString();

        var src =
            $$"""

              {{indentation}}public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
              {{indentation}}{
              {{indentation}}    return sourceType == typeof({{primitiveType}}) || sourceType == typeof(string);
              {{indentation}}}

              {{indentation}}public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
              {{indentation}}{
              {{indentation}}    if (value is {{primitiveType}} primitiveValue)
              {{indentation}}    {
              {{indentation}}        return {{context.TypeName}}.Create(primitiveValue);
              {{indentation}}    }

              {{indentation}}    if (value is string str)
              {{indentation}}    {
              {{indentation}}        {{ConvertFromStep.WritePrimitiveFromString(primitiveType)}}
              {{indentation}}        return {{context.TypeName}}.Create(primitive);
              {{indentation}}    }

              {{indentation}}    throw new NotSupportedException();
              {{indentation}}}
              """;

        builder.AppendLine(src);
    }

    private static string WritePrimitiveFromString(string primitiveType) =>
        primitiveType == "string"
            ? "var primitive = str;"
            : $"var primitive = {primitiveType}.Parse(str);";
}