using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordTypeDeclarationStep : ScopeStartStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string accessModifier = Util.AccessModifierFor(context.ValueObjectSymbol);
        string readonlyValue = context.ValueObjectSymbol.IsValueType ? " readonly" : string.Empty;
        string structValue = context.ValueObjectSymbol.IsValueType ? " struct" : string.Empty;

        builder.AppendLine($"""
                            {context.Indentation}{accessModifier}{readonlyValue} partial record{structValue} {context.ValueObjectSymbol.Name}
                            {context.Indentation}    : IComparable<{context.ValueObjectSymbol.Name}>, IComparable
                            #if NET7_0_OR_GREATER
                            {context.Indentation}        , IParsable<{context.ValueObjectSymbol.Name}>
                            #endif
                            """);
    }
}