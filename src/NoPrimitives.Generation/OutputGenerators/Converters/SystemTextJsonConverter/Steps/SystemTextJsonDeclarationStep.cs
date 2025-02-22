using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.SystemTextJsonConverter.Steps;

internal class SystemTextJsonDeclarationStep : ScopeStartStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string accessModifier = Util.AccessModifierFor(context.Item.ValueObject);

        var src =
            $"{context.Indentation}{accessModifier} class {context.TypeName}SystemTextJsonConverter : JsonConverter<{context.TypeName}>";

        builder.AppendLine(src);
    }
}