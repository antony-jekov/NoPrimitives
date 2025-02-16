using System.Linq;
using Microsoft.CodeAnalysis;


namespace NoPrimitives.Generation.OutputGenerators;

internal static class Util
{
    internal static string AccessModifierFor(INamedTypeSymbol symbol) =>
        symbol.DeclaredAccessibility == Accessibility.Public ? "public" : "internal";

    

    internal static ITypeSymbol ExtractTypeFromNullableType(ITypeSymbol typeSymbol) =>
        typeSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T &&
        typeSymbol is INamedTypeSymbol { TypeArguments.Length: 1 } namedTypeSymbol
            ? namedTypeSymbol.TypeArguments[0]
            : typeSymbol;

    internal static bool IsNumericType(ITypeSymbol typeSymbol)
    {
        typeSymbol = Util.ExtractTypeFromNullableType(typeSymbol);

        return typeSymbol.SpecialType is SpecialType.System_Byte or SpecialType.System_SByte
                   or SpecialType.System_Int16 or SpecialType.System_UInt16
                   or SpecialType.System_Int32 or SpecialType.System_UInt32
                   or SpecialType.System_Int64 or SpecialType.System_UInt64
                   or SpecialType.System_Decimal or SpecialType.System_Double
                   or SpecialType.System_Single or SpecialType.System_Char
               || typeSymbol.AllInterfaces
                   .Any(i => i.ToDisplayString().StartsWith("System.Numerics.INumber<"));
    }

    internal static bool IsDateOrTimeType(ITypeSymbol typeSymbol)
    {
        typeSymbol = Util.ExtractTypeFromNullableType(typeSymbol);

        return (typeSymbol.ContainingNamespace?.ToDisplayString() == "System" &&
                typeSymbol.Name is "DateTime" or "TimeSpan" or "DateOnly" or "TimeOnly" or "DateTimeOffset")
               || typeSymbol.BaseType?.ToDisplayString() == "System.DateTime";
    }

    internal static Integrations? ExtractGlobalIntegrations(IAssemblySymbol assembly) =>
        Util.ExtractIntegrations(assembly);

    internal static Integrations ExtractValueObjectIntegrations(INamedTypeSymbol valueObject,
        Integrations? globalIntegrations) =>
        Util.ExtractIntegrations(valueObject) ?? globalIntegrations ?? Integrations.Default;

    private static Integrations? ExtractIntegrations(ISymbol symbol) =>
        symbol.GetAttributes()
            .FirstOrDefault(Util.IsValueObjectAttribute)
            ?.ConstructorArguments
            .Where(arg => arg.Value is int intValue && intValue > 0)
            .Select(arg =>
            {
                if (arg.Value is int intValue)
                {
                    return (Integrations)intValue;
                }

                return new Integrations?();
            })
            .FirstOrDefault()
        ?? null;

    internal static bool IsValueObjectAttribute(AttributeData ad) =>
        ad.AttributeClass?.ToString().StartsWith("NoPrimitives.ValueObject") ?? false;
}