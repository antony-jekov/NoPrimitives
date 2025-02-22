using Microsoft.CodeAnalysis;


namespace NoPrimitives.Generation.Tests;

public class SystemTextJsonGeneratorTests : GeneratorTestBase
{
    [Fact]
    public void Conversion_ShouldConvertToAndFromJson()
    {
        const string src = """
                           using NoPrimitives;


                           namespace SomeNamespace;

                           [ValueObject<string>(Integrations.SystemTextJson)]
                           internal partial record Username;
                           """;

        Compilation compilation = GeneratorTestBase.GenerateSource(src);

        compilation.GetDiagnostics().Should().BeEmpty();

        if (compilation.SyntaxTrees.Count() != 3)
        {
            compilation.SyntaxTrees.First().GetText().Should().Be("baba");
            return;
        }

        compilation.SyntaxTrees.Should().HaveCount(3);

        compilation.SyntaxTrees.Last().FilePath.Should()
            .ContainAll("NoPrimitives", "SystemTextJson", ".g.cs", "Username", "SomeNamespace");
    }
}