using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordFactoryMethodStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string indentation = context.Indentation;

        builder.AppendLine($$"""

                             {{indentation}}public static {{context.Item.ValueObject.Name}} Create({{context.PrimitiveTypeName}} value)
                             {{indentation}}{
                             {{indentation}}    return new {{context.Item.ValueObject.Name}}(value);
                             {{indentation}}}
                             """);
    }
}