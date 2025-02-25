namespace NoPrimitives.NewtonsoftJson.Tests;

[ValueObject<byte>]
internal partial record ByteValueObject;

[ValueObject<sbyte>]
internal partial record SByteValueObject;

[ValueObject<short>]
internal partial record ShortValueObject;

[ValueObject<ushort>]
internal partial record UShortValueObject;

[ValueObject<int>]
internal partial record IntValueObject;

[ValueObject<uint>]
internal partial record UIntValueObject;

[ValueObject<long>]
internal partial record LongValueObject;

[ValueObject<ulong>]
internal partial record ULongValueObject;

[ValueObject<float>]
internal partial record FloatValueObject;

[ValueObject<double>]
internal partial record DoubleValueObject;

[ValueObject<decimal>]
internal partial record DecimalValueObject;

[ValueObject<DateTime>]
internal partial record DateTimeValueObject;

[ValueObject<DateOnly>]
internal partial record DateOnlyValueObject;

[ValueObject<TimeOnly>]
internal partial record TimeOnlyValueObject;

[ValueObject<DateTimeOffset>]
internal partial record DateTimeOffsetValueObject;

[ValueObject<Guid>]
internal partial record GuidValueObject;

[ValueObject(typeof(string))]
internal partial record StringValueObject;