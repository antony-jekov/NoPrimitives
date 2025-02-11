using Microsoft.CodeAnalysis;


namespace NoPrimitives.OutputGenerators;

internal sealed class RecordGenerator : OutputGeneratorBase
{
    protected override string Render(INamedTypeSymbol symbol, ITypeSymbol typeSymbol)
    {
        string accessModifier = symbol.DeclaredAccessibility == Accessibility.Public ? "public" : "internal";
        string primitiveType = typeSymbol.ToDisplayString();

        return $$"""
                 {{RecordGenerator.WriteNamespace(symbol)}}
                 {
                     using System;
                     using System.ComponentModel;
                     using System.CodeDom.Compiler;
                     using System.Diagnostics.CodeAnalysis;
                 
                 
                     [GeneratedCode("NoPrimitives", "{{References.Assembly.Value.GetName().Version}}")]
                     [ExcludeFromCodeCoverage(Justification = "Generated Code")]
                     {{RecordGenerator.WriteTypeDeclaration(symbol, accessModifier)}} : IComparable<{{symbol.Name}}>, IComparable
                     {
                         private {{symbol.Name}}({{primitiveType}} value)
                         {
                             this.Value = value;
                         }
                 
                         public {{primitiveType}} Value { get; }

                 {{RecordGenerator.WriteFactoryMethod(symbol, primitiveType)}}

                 {{RecordGenerator.WriteImplicitOperators(symbol, primitiveType)}}

                 {{RecordGenerator.WriteCompareTo(symbol, typeSymbol)}}

                 {{RecordGenerator.WriteToString(typeSymbol)}}
                 {{RecordGenerator.WriteRelationalOperators(symbol, typeSymbol)}}
                     }
                 }
                 """;
    }

    private static string WriteNamespace(INamedTypeSymbol symbol) =>
        !symbol.ContainingNamespace.IsGlobalNamespace
            ? $"namespace {symbol.ContainingNamespace.ToDisplayString()}"
            : string.Empty;

    private static string WriteTypeDeclaration(INamedTypeSymbol symbol, string accessModifier)
    {
        string readonlyValue = symbol.IsValueType ? " readonly" : string.Empty;
        string structValue = symbol.IsValueType ? " struct" : string.Empty;

        return $"{accessModifier}{readonlyValue} partial record{structValue} {symbol.Name}";
    }

    private static string WriteFactoryMethod(INamedTypeSymbol symbol, string primitiveType) =>
        $$"""
                  public static {{symbol.Name}} Create({{primitiveType}} value)
                  {
                      return new {{symbol.Name}}(value);
                  }
          """;

    private static string WriteImplicitOperators(INamedTypeSymbol symbol, string primitiveType) =>
        $$"""
                  public static implicit operator {{symbol.Name}}({{primitiveType}} value)
                  {
                      return Create(value);
                  }
              
                  public static implicit operator {{primitiveType}}({{symbol.Name}} vo)
                  {
                      return vo.Value;   
                  }
          """;

    private static string WriteToString(ITypeSymbol typeSymbol)
    {
        bool isNullable = typeSymbol.NullableAnnotation == NullableAnnotation.Annotated;
        string fallbackValue = isNullable ? " ?? string.Empty" : string.Empty;
        string conditionalAccessValue = isNullable ? "?" : string.Empty;

        return $$"""
                         public override string ToString()
                         {
                             return this.Value{{conditionalAccessValue}}.ToString(){{fallbackValue}};
                         }
                 """;
    }

    private static string WriteCompareTo(INamedTypeSymbol symbol, ITypeSymbol typeSymbol)
    {
        bool isNullable = typeSymbol.NullableAnnotation == NullableAnnotation.Annotated;
        string fallbackValue = isNullable ? " ?? -1" : string.Empty;
        string conditionalAccessValue = isNullable ? "?" : string.Empty;

        return $$"""
                         public int CompareTo({{symbol.Name}} other)
                         {
                             return this.Value{{conditionalAccessValue}}.CompareTo(other.Value){{fallbackValue}};
                         }
                     
                         public int CompareTo(object other)
                         {
                             if (other is null) return 1;
                             if (other is not {{symbol.Name}} vo) return 0;
                         
                             return CompareTo(vo);
                         }
                 """;
    }

    private static string WriteRelationalOperators(INamedTypeSymbol symbol, ITypeSymbol typeSymbol) =>
        OutputGeneratorBase.IsNumericType(typeSymbol) || OutputGeneratorBase.IsDateOrTimeType(typeSymbol)
            ? $$"""
                
                        public static bool operator > ({{symbol.Name}} left, {{symbol.Name}} right)
                        {
                            return left.Value > right.Value;
                        }
                        
                        public static bool operator < ({{symbol.Name}} left, {{symbol.Name}} right)
                        {
                            return left.Value < right.Value;
                        }
                        
                        public static bool operator >= ({{symbol.Name}} left, {{symbol.Name}} right)
                        {
                            return left.Value >= right.Value;
                        }
                        
                        public static bool operator <= ({{symbol.Name}} left, {{symbol.Name}} right)
                        {
                            return left.Value <= right.Value;
                        }
                """
            : string.Empty;
}