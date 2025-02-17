using System.Text;
using NoPrimitives.Rendering;
using NoPrimitives.Rendering.Steps;


namespace NoPrimitives.Generation.OutputGenerators.Records.Steps;

internal class RecordImplicitOperatorsStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        string indentation = context.Indentation;

        builder.AppendLine($$"""

                             {{indentation}}public static implicit operator {{context.Item.ValueObject.Name}}({{context.PrimitiveTypeName}} value)
                             {{indentation}}{
                             {{indentation}}    return Create(value);
                             {{indentation}}}

                             {{indentation}}public static implicit operator {{context.PrimitiveTypeName}}({{context.Item.ValueObject.Name}} vo)
                             {{indentation}}{
                             {{indentation}}    return vo.Value;   
                             {{indentation}}}
                             """);
    }
}