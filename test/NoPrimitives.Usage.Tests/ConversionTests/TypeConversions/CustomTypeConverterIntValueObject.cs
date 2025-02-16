using System.ComponentModel;
using System.Globalization;


namespace NoPrimitives.Usage.Tests.ConversionTests.TypeConversions;

[ValueObject<int>]
internal partial record CustomTypeConverterIntValueObject;

internal class Always255Converter : CustomTypeConverterIntValueObjectTypeConverter
{
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) =>
        CustomTypeConverterIntValueObject.Create(255);

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType)
    {
        if (destinationType == typeof(int))
        {
            return 255;
        }

        if (destinationType == typeof(string))
        {
            return "255";
        }

        throw new NotSupportedException();
    }
}