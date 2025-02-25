namespace NoPrimitives.NewtonsoftJson.Tests;

[ValueObject<byte?>]
internal partial record struct NullByteValueObject;

[ValueObject<sbyte?>]
internal partial record struct NullSByteValueObject;

[ValueObject<short?>]
internal partial record struct NullShortValueObject;

[ValueObject<ushort?>]
internal partial record struct NullUShortValueObject;

[ValueObject<int?>]
internal partial record struct NullIntValueObject;

[ValueObject<uint?>]
internal partial record struct NullUIntValueObject;

[ValueObject<long?>]
internal partial record struct NullLongValueObject;

[ValueObject<ulong?>]
internal partial record struct NullULongValueObject;

[ValueObject<float?>]
internal partial record struct NullFloatValueObject;

[ValueObject<double?>]
internal partial record struct NullDoubleValueObject;

[ValueObject<decimal?>]
internal partial record struct NullDecimalValueObject;

[ValueObject<DateTime?>]
internal partial record struct NullDateTimeValueObject;

[ValueObject<DateOnly?>]
internal partial record struct NullDateOnlyValueObject;

[ValueObject<TimeOnly?>]
internal partial record struct NullTimeOnlyValueObject;

[ValueObject<DateTimeOffset?>]
internal partial record struct NullDateTimeOffsetValueObject;

[ValueObject<Guid?>]
internal partial record struct NullGuidValueObject;