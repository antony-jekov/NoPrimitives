﻿using System.Text;
using NoPrimitives.RenderPipeline;
using NoPrimitives.RenderPipeline.Steps;


namespace NoPrimitives.OutputGenerators.Records.Steps;

internal class RecordImplicitOperatorsStep : ScopedRenderStep
{
    protected override void Render(RenderContext context, StringBuilder builder)
    {
        builder.AppendLine($$"""

                             {{context.Indentation}}public static implicit operator {{context.ValueObjectSymbol.Name}}({{context.PrimitiveTypeName}} value)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return Create(value);
                             {{context.Indentation}}}

                             {{context.Indentation}}public static implicit operator {{context.PrimitiveTypeName}}({{context.ValueObjectSymbol.Name}} vo)
                             {{context.Indentation}}{
                             {{context.Indentation}}    return vo.Value;   
                             {{context.Indentation}}}
                             """);
    }
}