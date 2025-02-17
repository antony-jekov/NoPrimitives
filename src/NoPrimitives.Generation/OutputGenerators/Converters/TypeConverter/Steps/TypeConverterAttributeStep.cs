using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;

internal class TypeConverterAttributeStep : AttributeStep
{
    protected override string RenderAttribute(RenderContext context) =>
        $"System.ComponentModel.TypeConverter(typeof({context.Item.ValueObject.Name}TypeConverter))";
}