using System.Text;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;


namespace NoPrimitives.OutputGenerators.Records.Steps;

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
                            """);
    }
}