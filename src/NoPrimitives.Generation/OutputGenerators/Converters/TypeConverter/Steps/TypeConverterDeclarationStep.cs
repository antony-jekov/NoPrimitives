using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter.Steps;

internal class TypeConverterDeclarationStep : ScopeStartStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string accessModifier = Util.AccessModifierFor(context.Item.ValueObject);

        var src =
            $"{context.Indentation}{accessModifier} class {context.TypeName}TypeConverter : TypeConverter";

        builder.AppendLine(src);
    }
}