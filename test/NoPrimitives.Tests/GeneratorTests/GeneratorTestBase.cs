using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace NoPrimitives.Tests.GeneratorTests;

public abstract class GeneratorTestBase
{
    protected static (Compilation compilation, ImmutableArray<Diagnostic> diagnostics) GenerateSource(string source)
    {
        Compilation compilation = GeneratorTestBase.CreateCompilation(source);

        var generator = new ValueObjectGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);

        driver.RunGeneratorsAndUpdateCompilation(compilation,
            out Compilation updatedCompilation,
            out ImmutableArray<Diagnostic> diagnostics);

        return (updatedCompilation, diagnostics);
    }

    private static CSharpCompilation CreateCompilation(string source, [CallerMemberName] string testName = "") =>
        CSharpCompilation.Create(
            testName,
            [CSharpSyntaxTree.ParseText(source)],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ValueObjectAttribute).Assembly.Location),
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
}