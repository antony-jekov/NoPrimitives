using System;
using System.Text;
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

        bool alreadyHasTypeConverterAttribute =
            Util.AlreadyHasAttributeStartingWith(context.ValueObjectSymbol, "System.ComponentModel.TypeConverter");

        if (!alreadyHasTypeConverterAttribute)
        {
            builder.AppendLine(
                $"{context.Indentation}[System.ComponentModel.TypeConverter(typeof({context.ValueObjectSymbol.Name}TypeConverter))]"
            );
        }
    }
}