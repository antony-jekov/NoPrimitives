using System.Text;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;


namespace NoPrimitives.OutputGenerators.Records.Steps;

internal class RecordFactoryMethodStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        builder.AppendLine($$"""

                             {{context.Indentation}}public static {{context.ValueObjectSymbol.Name}} Create({{context.PrimitiveTypeName}} value)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return new {{context.ValueObjectSymbol.Name}}(value);
                             {{context.Indentation}}}
                             """);
    }
}