using NoPrimitives.Rendering;


namespace NoPrimitives.Generation.OutputGenerators.Converters.TypeConverter;

internal class TypeConverterGenerator() : OutputGeneratorBase("TypeConverter")
{
    protected override string Render(RenderItem item)
    {
        string accessModifier = Util.AccessModifierFor(item.ValueObject);
        string primitiveType = Util.ExtractTypeFromNullableType(item.Primitive).ToDisplayString();
        string typeName = item.ValueObject.Name;

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

        return OutputGeneratorBase.WrapInNamespaceFor(item.ValueObject, source);
    }

    private static string WritePrimitiveFromString(string primitiveType) =>
        primitiveType == "string"
            ? "var primitive = str;"
            : $"var primitive = {primitiveType}.Parse(str);";
}