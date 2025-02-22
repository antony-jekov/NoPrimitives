using System;
using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter.Steps;

internal class SystemTextJsonDeserializeStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string primitiveType = Util.ExtractTypeFromNullableType(context.Item.Primitive)
            .ToDisplayString();

        string indentation = context.Indentation;

        var src =
            $$"""

              {{indentation}}public override {{context.TypeName}} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
              {{indentation}}{{{SystemTextJsonDeserializeStep.GetNullDeserializeSource(context)}}
              {{indentation}}    var value = {{SystemTextJsonDeserializeStep.GetValueSource(primitiveType)}};
              {{indentation}}    return {{context.TypeName}}.Create(value);
              {{indentation}}}
              """;

        builder.AppendLine(src);
    }

    private static string GetNullDeserializeSource(RenderContext context) =>
        !context.PrimitiveTypeName.EndsWith("?")
            ? string.Empty
            : $$"""

                if (reader.TokenType == JsonTokenType.Null)
                {
                    return {{context.TypeName}}.Create(null);
                }
                """;

    private static string GetValueSource(string primitiveType) =>
        primitiveType switch
        {
            "bool" => "reader.GetBoolean()",
            "byte" => "reader.GetByte()",
            "sbyte" => "reader.GetSByte()",
            "decimal" => "reader.GetDecimal()",
            "double" => "reader.GetDouble()",
            "float" => "reader.GetSingle()",
            "int" => "reader.GetInt32()",
            "long" => "reader.GetInt64()",
            "uint" => "reader.GetUInt32()",
            "ulong" => "reader.GetUInt64()",
            "short" => "reader.GetInt16()",
            "ushort" => "reader.GetUInt16()",
            "string" => "reader.GetString()",
            "System.Guid" => "reader.GetGuid()",
            "System.DateTime" => "reader.GetDateTime()",
            "System.DateTimeOffset" => "DateTimeOffset.Parse(reader.GetString())",
            "System.DateOnly" => "DateOnly.Parse(reader.GetString())",
            "System.TimeOnly" => "TimeOnly.Parse(reader.GetString())",
            _ => throw new NotSupportedException($"Unsupported primitive type: {primitiveType}"),
        };
}