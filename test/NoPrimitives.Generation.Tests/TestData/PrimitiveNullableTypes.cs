﻿using System.Collections;


namespace NoPrimitives.Generation.Tests.TestData;

public class PrimitiveNullableTypes : IEnumerable<object[]>
{
    private static readonly List<object[]> Types =
    [
        ["Byte", "byte?"],
        ["SByte", "sbyte?"],
        ["Short", "short?"],
        ["Ushort", "ushort?"],
        ["Int", "int?"],
        ["UInt", "uint?"],
        ["Long", "long?"],
        ["ULong", "ulong?"],
        ["Float", "float?"],
        ["Double", "double?"],
        ["Decimal", "decimal?"],
        ["Bool", "bool?"],
        ["Guid", "System.Guid?"],
        ["DateTime", "System.DateTime?"],
        ["DateTimeOffset", "System.DateTimeOffset?"],
        ["DateOnly", "System.DateOnly?"],
        ["TimeOnly", "System.TimeOnly?"],
    ];

    public IEnumerator<object[]> GetEnumerator() =>
        PrimitiveNullableTypes.Types.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}