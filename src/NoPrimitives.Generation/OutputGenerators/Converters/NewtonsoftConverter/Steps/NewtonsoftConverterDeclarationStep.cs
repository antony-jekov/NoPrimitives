using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Converters.NewtonsoftConverter.Steps;

internal class NewtonsoftConverterDeclarationStep : ScopeStartStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string accessModifier = Util.AccessModifierFor(context.Item.ValueObject);
        var src =
            $"{context.Indentation}{accessModifier} class {context.TypeName}NewtonsoftJsonConverter: JsonConverter<{context.TypeName}>";

        builder.AppendLine(src);
    }
}