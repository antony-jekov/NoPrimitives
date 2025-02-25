using Microsoft.CodeAnalysis;
using NoPrimitives.Generation.Tests.TestData;


namespace NoPrimitives.Generation.Tests;

public class NewtonsoftJsonGeneratorTests : GeneratorTestBase
{
    [Theory]
    [ClassData(typeof(PrimitiveTypes))]
    [ClassData(typeof(PrimitiveNullableTypes))]
    public void WhenNewtonsoftIntegration_IntegratesWithNewtonsoftJson(
        string valueObjectName, string primitiveType)
    {
        var src = $"""
                   using NoPrimitives;

                   namespace SomeNamespace;

                   [ValueObject<{primitiveType}>(Integrations.NewtonsoftJson)]
                   public partial record {valueObjectName};
                   """;

        Compilation compilation = GeneratorTestBase.GenerateSource(src);

        compilation.GetDiagnostics().Should().BeEmpty();

        compilation.SyntaxTrees.Should().HaveCount(3);
        SyntaxTree newtonsoftSyntax = compilation.SyntaxTrees.Last();

        newtonsoftSyntax.FilePath.Should().Contain("NewtonsoftJson");
    }
}