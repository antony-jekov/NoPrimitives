using System;
using System.Text;
using NoPrimitives.Core;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordAttributesStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        Version? assemblyVersion = References.Assembly.Value.GetName().Version;

        builder.Append("\n\n");
        builder.AppendLine($"""{context.Indentation}[GeneratedCode("NoPrimitives", "{assemblyVersion}")]""");

        builder.AppendLine($"""{context.Indentation}[ExcludeFromCodeCoverage(Justification = "Generated Code")]""");

        var typeConverterAttribute =
            $"System.ComponentModel.TypeConverter(typeof({context.Item.ValueObject.Name}TypeConverter))";

        RecordAttributesStep.AddAttributeIfNotPresent(context, typeConverterAttribute, builder);
    }

    private static void AddForIntegrations(RenderContext context, Integrations integrations, StringBuilder builder)
    {
        string symbolName = context.Item.ValueObject.Name;

        if (integrations.HasFlag(Integrations.NewtonsoftJson))
        {
            var attribute = $"Newtonsoft.Json.JsonConverter(typeof({symbolName}NewtonsoftConverter))";

            //RecordAttributesStep.AddAttributeIfNotPresent(context, attribute, builder);
        }
    }

    private static void AddAttributeIfNotPresent(RenderContext context, string attribute, StringBuilder builder)
    {
        string attributeFullName = RecordAttributesStep.ExtractAttributeFullName(attribute);

        if (Util.AlreadyHasAttributeStartingWith(context.Item.ValueObject, attributeFullName))
        {
            return;
        }

        builder.AppendLine($"{context.Indentation}[{attribute}]");
    }

    private static string ExtractAttributeFullName(string attribute)
    {
        int indexOf = attribute.IndexOf('(');
        return attribute.Substring(0, indexOf);
    }
}