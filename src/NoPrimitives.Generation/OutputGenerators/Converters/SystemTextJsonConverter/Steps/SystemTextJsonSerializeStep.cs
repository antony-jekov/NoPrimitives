using System;
using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter.Steps;

internal class SystemTextJsonSerializeStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string primitiveType = Util.ExtractTypeFromNullableType(context.Item.Primitive)
            .ToDisplayString();

        string writeNullSource = SystemTextJsonSerializeStep.RenderWriteNullSource(context.Indent());
        bool isNullablePrimitiveType = context.PrimitiveTypeName.EndsWith("?");
        string serializationSource = isNullablePrimitiveType
            ? SystemTextJsonSerializeStep.GetNullableValueWriteSource(primitiveType)
            : SystemTextJsonSerializeStep.GetValueWriteSource(primitiveType);

        string indentation = context.Indentation;

        var src =
            $$"""
              {{indentation}}public override void Write(Utf8JsonWriter writer, {{context.TypeName}} value, JsonSerializerOptions options)
              {{indentation}}{
              {{writeNullSource}}
              {{indentation}}    {{serializationSource}};
              {{indentation}}}
              """;

        builder.AppendLine(src);
    }

    private static string RenderWriteNullSource(RenderContext context)
    {
        if (!context.PrimitiveTypeName.EndsWith("?"))
        {
            return string.Empty;
        }

        string indentation = context.Indentation;

        return $$"""
                 {{indentation}}if (!value.Value.HasValue)
                 {{indentation}}{
                 {{indentation}}    writer.WriteNullValue();
                 {{indentation}}    return;
                 {{indentation}}}
                 """;
    }

    private static string GetNullableValueWriteSource(string primitiveType) =>
        primitiveType switch
        {
            "bool" => "writer.WriteBooleanValue(value.Value.Value)",
            "byte" or "sbyte" or "decimal" or "double" or "float"
                or "int" or "long" or "uint" or "ulong" or "short"
                or "ushort" => "writer.WriteNumberValue(value.Value.Value)",
            "string" or "System.Guid" or "System.DateTime" or "System.DateTimeOffset" =>
                "writer.WriteStringValue(value.Value.Value)",
            "System.DateOnly" or "System.TimeOnly" =>
                "writer.WriteStringValue(value.Value.Value.ToString(\"O\"))",
            _ => throw new NotSupportedException($"Unsupported primitive type: {primitiveType}"),
        };

    private static string GetValueWriteSource(string primitiveType) =>
        primitiveType switch
        {
            "bool" => "writer.WriteBooleanValue(value.Value)",
            "byte" or "sbyte" or "decimal" or "double" or "float"
                or "int" or "long" or "uint" or "ulong" or "short"
                or "ushort" => "writer.WriteNumberValue(value.Value)",
            "string" or "System.Guid" or "System.DateTime" or "System.DateTimeOffset" =>
                "writer.WriteStringValue(value.Value)",
            "System.DateOnly" or "System.TimeOnly" =>
                "writer.WriteStringValue(value.Value.ToString(\"O\"))",
            _ => throw new NotSupportedException($"Unsupported primitive type: {primitiveType}"),
        };
}