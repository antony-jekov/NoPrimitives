using Microsoft.CodeAnalysis;


namespace NoPrimitives.Generation.Tests;

public class NewtonsoftJsonGeneratorTests : GeneratorTestBase
{
    [Fact]
    public void WhenNewtonsoftIntegration_IntegratesWithNewtonsoftJson()
    {
        const string src = """
                           using NoPrimitives;

                           namespace SomeNamespace;

                           [ValueObject<int>(Integrations.NewtonsoftJson)]
                           public partial record SomeValueObject;
                           """;

        Compilation compilation = GeneratorTestBase.GenerateSource(src);

        compilation.GetDiagnostics().Should().BeEmpty();

        compilation.SyntaxTrees.Should().HaveCount(3);
        SyntaxTree newtonsoftSyntax = compilation.SyntaxTrees.Last();

        newtonsoftSyntax.FilePath.Should().Contain("NewtonsoftJson");
    }
}