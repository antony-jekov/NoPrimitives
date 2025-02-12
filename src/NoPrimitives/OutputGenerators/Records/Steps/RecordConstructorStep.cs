using System.Text;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;


namespace NoPrimitives.OutputGenerators.Records.Steps;

internal class RecordConstructorStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string primitiveType = context.PrimitiveTypeSymbol.ToDisplayString();

        builder.AppendLine(
            $$"""

              {{context.Indentation}}private {{context.ValueObjectSymbol.Name}}({{primitiveType}} value)
              {{context.Indentation}}{
              {{context.Indentation}}    this.Value = value;
              {{context.Indentation}}}
              {{context.Indentation}}
              {{context.Indentation}}public {{primitiveType}} Value { get; }
              """);
    }
}