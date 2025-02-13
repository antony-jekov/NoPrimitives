using Microsoft.CodeAnalysis;


namespace NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;

internal class TypeConverterGenerator() : OutputGeneratorBase("TypeConverter")
{
    protected override string Render(INamedTypeSymbol symbol, ITypeSymbol typeSymbol)
    {
        string accessModifier = Util.AccessModifierFor(symbol);
        string primitiveType = Util.ExtractTypeFromNullableType(typeSymbol).ToDisplayString();
        string typeName = symbol.Name;

        var source =
            $$"""
                  using System;
                  using System.Globalization;
                  using System.ComponentModel;
              
              
                  {{accessModifier}} class {{typeName}}TypeConverter : TypeConverter
                  {
                      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
                      {
                          return sourceType == typeof({{primitiveType}}) || sourceType == typeof(string);
                      }
                      
                      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
                      {
                          if (value is {{primitiveType}} primitiveValue)
                          {
                              return {{typeName}}.Create(primitiveValue);
                          }
                  
                          if (value is string str)
                          {
                              {{TypeConverterGenerator.WritePrimitiveFromString(primitiveType)}}
                              return {{typeName}}.Create(primitive);
                          }
                          
                          throw new NotSupportedException();
                      }
                  
                      public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
                      {
                          return sourceType == typeof({{primitiveType}}) || sourceType == typeof(string);
                      }
                  
                      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
                      {
                          if (value is not {{typeName}} vo) {
                              throw new NotSupportedException();
                          }
                          
                          if (destinationType == typeof(string))
                          {
                               return vo.ToString();
                          }
                          
                          if (destinationType == typeof({{primitiveType}}))
                          {
                              return vo.Value;
                          }
                          
                          throw new NotSupportedException();
                      }
                  }
              """;

        return OutputGeneratorBase.WrapInNamespaceFor(symbol, source);
    }

    private static string WritePrimitiveFromString(string primitiveType) =>
        primitiveType == "string"
            ? "var primitive = str;"
            : $"var primitive = {primitiveType}.Parse(str);";
}