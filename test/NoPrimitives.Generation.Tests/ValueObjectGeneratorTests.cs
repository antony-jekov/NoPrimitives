using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Generation.Tests.TestData;


namespace NoPrimitives.Generation.Tests;

public class ValueObjectGeneratorTests : GeneratorTestBase
{
    [Theory]
    [ClassData(typeof(PrimitiveTypes))]
    [ClassData(typeof(PrimitiveNullableTypes))]
    public void Execute_WhenGivenSupportedPrimitiveValueWithGenerics_GeneratesDefaultTrees(
        string valueObjectName,
        string primitiveType)
    {
        string source = ValueObjectGeneratorTests.GenerateDefaultGenericSource(valueObjectName, primitiveType);

        Compilation compilation = GeneratorTestBase.GenerateSource(source);

        ValueObjectGeneratorTests.AssertDefaultGeneration(compilation, valueObjectName);
    }

    [Theory]
    [ClassData(typeof(PrimitiveTypes))]
    public void Execute_WhenGivenSupportedPrimitiveValueWithTypeOf_GeneratesDefaultTrees(
        string valueObjectName,
        string primitiveType)
    {
        string source = ValueObjectGeneratorTests.GenerateDefaultTypeOfSource(valueObjectName, primitiveType);

        Compilation compilation = GeneratorTestBase.GenerateSource(source);

        ValueObjectGeneratorTests.AssertDefaultGeneration(compilation, valueObjectName);
    }

    private static void AssertDefaultGeneration(Compilation compilation, string valueObjectName)
    {
        compilation.GetDiagnostics().Should().BeEmpty();
        compilation.SyntaxTrees.Should().HaveCount(4);

        // verify value object generation
        compilation.SyntaxTrees.ElementAt(1).FilePath
            .Should()
            .ContainAll(valueObjectName, "Some.Namespace", "NoPrimitives", ".g.cs");

        // verify default type converter
        compilation.SyntaxTrees.ElementAt(2).FilePath
            .Should()
            .ContainAll(valueObjectName, "Some.Namespace", "NoPrimitives", "TypeConverter", ".g.cs");

        // verify default json converter is generated
        compilation.SyntaxTrees.ElementAt(3).FilePath
            .Should()
            .ContainAll("NoPrimitives", "SystemTextJson", ".g.cs", valueObjectName, "Some.Namespace");
    }

    [Theory]
    [ClassData(typeof(RelatableTypes))]
    public async Task Execute_WhenGivenRecordWithRelativeType_ItGeneratesRelativeOperators(string primitiveType)
    {
        var source = $"""
                      using System;
                      using NoPrimitives;

                      namespace Some.Namespace;

                      [ValueObject<{primitiveType}>]
                      internal partial record MyValueObject;
                      """;

        Compilation compilation = GeneratorTestBase.GenerateSource(source);

        compilation.SyntaxTrees.Should().HaveCount(4);
        SyntaxTree syntaxTree = compilation.SyntaxTrees.ElementAt(1);
        SyntaxNode root = await syntaxTree.GetRootAsync();

        ImmutableArray<BinaryExpressionSyntax> binaryExpressionSyntaxes =
            [..root.DescendantNodes().OfType<BinaryExpressionSyntax>()];

        ValueObjectGeneratorTests.GetOperatorExpressionsFor(binaryExpressionSyntaxes, SyntaxKind.GreaterThanToken)
            .Should().NotBeEmpty();

        ValueObjectGeneratorTests.GetOperatorExpressionsFor(binaryExpressionSyntaxes, SyntaxKind.GreaterThanEqualsToken)
            .Should().NotBeEmpty();
    }

    private static string GenerateDefaultTypeOfSource(string valueObjectName, string primitiveType) =>
        $"""
         using NoPrimitives;

         namespace Some.Namespace;

         [ValueObject(typeof({primitiveType}))]
         internal partial record {valueObjectName};
         """;

    private static string GenerateDefaultGenericSource(string valueObjectName, string primitiveType) =>
        $"""
         using NoPrimitives;

         namespace Some.Namespace;

         [ValueObject<{primitiveType}>]
         internal partial record {valueObjectName};
         """;

    private static IEnumerable<BinaryExpressionSyntax> GetOperatorExpressionsFor(
        ImmutableArray<BinaryExpressionSyntax> expressionSyntaxes, SyntaxKind kind) =>
        expressionSyntaxes.Where(expr => expr.OperatorToken.IsKind(kind));
}