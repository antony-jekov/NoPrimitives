using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NoPrimitives.Tests.GeneratorTests.TestData;


namespace NoPrimitives.Tests.GeneratorTests;

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

        (Compilation compilation, ImmutableArray<Diagnostic> diagnostics) = GeneratorTestBase.GenerateSource(source);

        diagnostics.Should().BeEmpty();
        compilation.SyntaxTrees.Should().HaveCount(2);

        SyntaxTree generatedSyntaxTree = compilation.SyntaxTrees.Last();

        generatedSyntaxTree.FilePath.Should().ContainAll("MyValueObject", "Some.Namespace", "NoPrimitives", ".g.");
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

        (Compilation compilation, _) = GeneratorTestBase.GenerateSource(source);

        compilation.SyntaxTrees.Should().HaveCount(2);
        SyntaxTree syntaxTree = compilation.SyntaxTrees.Last();

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