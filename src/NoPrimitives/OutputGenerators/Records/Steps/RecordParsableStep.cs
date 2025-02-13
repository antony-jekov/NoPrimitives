using System.Text;
using Microsoft.CodeAnalysis;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;


namespace NoPrimitives.OutputGenerators.Records.Steps;

internal class RecordParsableStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string primitiveType = Util.ExtractTypeFromNullableType(context.PrimitiveTypeSymbol).ToDisplayString();
        INamedTypeSymbol symbol = context.ValueObjectSymbol;

        builder.AppendLine($"""

                            {RecordParsableStep.RenderParseSource(context.Indentation, symbol.Name, primitiveType)}
                                    
                            {RecordParsableStep.RenderTryParseSource(context.Indentation, symbol.Name, primitiveType)}
                            """);
    }

    private static string RenderParseSource(string indentation, string symbolName, string primitiveType)
    {
        string providerParamValue = primitiveType switch
        {
            "bool" or "char" => string.Empty,
            _ => ", provider",
        };

        string scopeSrc =
            primitiveType == "string"
                ? $"{indentation}    return {symbolName}.Create(str);"
                : $$"""
                    {{indentation}}    var primitive = {{primitiveType}}.Parse(str{{providerParamValue}});
                    {{indentation}}    return {{symbolName}}.Create(primitive);
                    """;

        return $$"""
                 {{indentation}}public static {{symbolName}} Parse(string str, IFormatProvider provider)
                 {{indentation}}{
                 {{scopeSrc}}
                 {{indentation}}}
                 """;
    }

    private static string RenderTryParseSource(string indentation, string symbolName, string primitiveType)
    {
        string scopeSrc = primitiveType == "string"
            ? $"""
               {indentation}    output = {symbolName}.Create(str);
               {indentation}    return true;
               """
            : $$"""
                {{indentation}}    if ({{RecordParsableStep.RenderTryParseCallFor(primitiveType)}})
                {{indentation}}    {
                {{indentation}}        output = {{symbolName}}.Create(primitiveValue);
                {{indentation}}        return true;
                {{indentation}}    }

                {{indentation}}    output = default;
                {{indentation}}    return false;
                """;

        return $$"""
                 {{indentation}}public static bool TryParse(string str, IFormatProvider provider, out {{symbolName}} output)
                 {{indentation}}{
                 {{scopeSrc}}
                 {{indentation}}}
                 """;
    }

    private static string RenderTryParseCallFor(string primitiveType)
    {
        string paramsValue = primitiveType switch
        {
            "bool" or "char" => "str, out var primitiveValue",
            _ => "str, provider, out var primitiveValue",
        };

        return $"{primitiveType}.TryParse({paramsValue})";
    }
}