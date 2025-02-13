namespace NoPrimitives.Usage.Tests.ConversionTests.TypeConversions;

public class CustomIntValueObjectTypeConversionTests
{
    private readonly Always255Converter _typeConverter = new();

    [Fact]
    public void CanConvert_ShouldBeImplementedCorrectly()
    {
        this._typeConverter.CanConvertFrom(null, typeof(int)).Should().BeTrue();
        this._typeConverter.CanConvertFrom(null, typeof(string)).Should().BeTrue();

        this._typeConverter.CanConvertTo(null, typeof(int)).Should().BeTrue();
        this._typeConverter.CanConvertTo(null, typeof(string)).Should().BeTrue();
    }

    [Fact]
    public void ConvertFrom_ShouldReturnCorrectValue()
    {
        this._typeConverter.ConvertFrom(null!, null!, 5).Should().Be(CustomTypeConverterIntValueObject.Create(255));
        this._typeConverter.ConvertFrom(null!, null!, "50").Should().Be(CustomTypeConverterIntValueObject.Create(255));
    }

    [Fact]
    public void ConvertTo_ShouldReturnCorrectValue()
    {
        this._typeConverter.ConvertTo(null, null, CustomTypeConverterIntValueObject.Create(50), typeof(int))
            .Should()
            .Be(255);

        this._typeConverter.ConvertTo(null, null, CustomTypeConverterIntValueObject.Create(5), typeof(string))
            .Should()
            .Be("255");
    }
}