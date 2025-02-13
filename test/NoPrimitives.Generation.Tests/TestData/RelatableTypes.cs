using System.Collections;


namespace NoPrimitives.Generation.Tests.TestData;

public class RelatableTypes : IEnumerable<object[]>
{
    private static List<object[]> RelatableTypesData =>
    [
        ["int"],
        ["int?"],
        ["long"],
        ["long?"],
        ["sbyte"],
        ["sbyte?"],
        ["short"],
        ["short?"],
        ["ushort"],
        ["ushort?"],
        ["uint"],
        ["uint?"],
        ["ulong"],
        ["ulong?"],
        ["float"],
        ["float?"],
        ["double"],
        ["double?"],
        ["decimal"],
        ["decimal?"],
        ["DateTime"],
        ["DateTime?"],
        ["DateTimeOffset"],
        ["DateTimeOffset?"],
        ["DateOnly"],
        ["DateOnly?"],
        ["TimeOnly"],
        ["TimeOnly?"],
    ];

    public IEnumerator<object[]> GetEnumerator() =>
        RelatableTypes.RelatableTypesData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}