using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Generation.Tests.TestData;


namespace NoPrimitives.Generation.Tests;

public class ValueObjectGeneratorTests : GeneratorTestBase
{
    [Fact]
    public void Execute_WhenGivenRecordWithValueObjectAttribute_ItGeneratesValueObject()
    {
        const string source = """
                              using NoPrimitives;

                              namespace Some.Namespace;

                              [ValueObject<string>]
                              internal partial record MyValueObject;
                              """;

        Compilation compilation = GeneratorTestBase.GenerateSource(source);

        compilation.GetDiagnostics().Should().BeEmpty();

        compilation.SyntaxTrees.Should().HaveCount(4);

        SyntaxTree generatedSyntaxTree = compilation.SyntaxTrees.ElementAt(1);

        generatedSyntaxTree.FilePath.Should().ContainAll("MyValueObject", "Some.Namespace", "NoPrimitives", ".g.cs");
    }

    [Fact]
    public void Execute_WhenGivenRecordWithValueObjectAttribute_ItGeneratesTypeConverter()
    {
        const string source = """
                              using NoPrimitives;

                              namespace Some.Namespace;

                              [ValueObject<string>]
                              internal partial record MyValueObject;
                              """;

        Compilation compilation = GeneratorTestBase.GenerateSource(source);

        compilation.GetDiagnostics().Should().BeEmpty();
        compilation.SyntaxTrees.Should().HaveCount(4);

        SyntaxTree generatedSyntaxTree = compilation.SyntaxTrees.ElementAt(2);

        generatedSyntaxTree.FilePath.Should()
            .ContainAll("MyValueObject.TypeConverter", "Some.Namespace", "NoPrimitives", ".g.cs");
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

    private static IEnumerable<BinaryExpressionSyntax> GetOperatorExpressionsFor(
        ImmutableArray<BinaryExpressionSyntax> expressionSyntaxes, SyntaxKind kind) =>
        expressionSyntaxes.Where(expr => expr.OperatorToken.IsKind(kind));
}