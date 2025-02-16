using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;


namespace NoPrimitives.Generation.Tests;

internal static class CSharpSourceCodeVerifier<TSourceCodeGenerator>
    where TSourceCodeGenerator : IIncrementalGenerator, new()
{
    internal class GenerationTest : CSharpSourceGeneratorTest<TSourceCodeGenerator, DefaultVerifier>
    {
    }
}