using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordTypeDeclarationStep : ScopeStartStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string accessModifier = Util.AccessModifierFor(context.Item.ValueObject);
        string readonlyValue = context.Item.ValueObject.IsValueType ? " readonly" : string.Empty;
        string structValue = context.Item.ValueObject.IsValueType ? " struct" : string.Empty;

        builder.AppendLine($"""
                            {context.Indentation}{accessModifier}{readonlyValue} partial record{structValue} {context.Item.ValueObject.Name}
                            {context.Indentation}    : IValueObject<{context.PrimitiveTypeName}>, IComparable<{context.Item.ValueObject.Name}>, IComparable
                            #if NET7_0_OR_GREATER
                            {context.Indentation}        , IParsable<{context.Item.ValueObject.Name}>
                            #endif
                            """);
    }
}