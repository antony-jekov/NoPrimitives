using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordConstructorStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string primitiveType = context.Item.Primitive.ToDisplayString();
        string indentation = context.Indentation;

        builder.AppendLine(
            $$"""
              {{indentation}}private {{context.Item.ValueObject.Name}}({{primitiveType}} value)
              {{indentation}}{
              {{indentation}}    this.Value = value;
              {{indentation}}}
              {{indentation}}
              {{indentation}}public {{primitiveType}} Value { get; }
              """);
    }
}