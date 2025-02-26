﻿using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Newtonsoft.Json;


namespace NoPrimitives.Generation.Tests;

public abstract class GeneratorTestBase
{
    private static readonly ImmutableArray<string> Locations =
    [
        typeof(ValueObjectAttribute).Assembly.Location.Replace(".dll", string.Empty),
        typeof(JsonConvert).Assembly.Location.Replace(".dll", string.Empty),
    ];

    private static readonly Lazy<ImmutableArray<MetadataReference>>
        DependencyAssemblies = new(GeneratorTestBase.ScanAssemblies);

    private static ImmutableArray<MetadataReference> ScanAssemblies() =>
        new ReferenceAssemblies(
                "net9.0",
                new PackageIdentity("Microsoft.NETCore.App.Ref", "9.0.0"),
                Path.Combine("ref", "net9.0")
            )
            .AddAssemblies(GeneratorTestBase.Locations)
            .ResolveAsync(LanguageNames.CSharp, CancellationToken.None).GetAwaiter().GetResult();

    protected static Compilation GenerateSource(
        string source,
        [CallerMemberName] string testName = "")
    {
        Compilation compilation = GeneratorTestBase.CreateCompilation(source, testName);

        var generator = new ValueObjectGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);

        driver.RunGeneratorsAndUpdateCompilation(compilation,
            out Compilation updatedCompilation,
            out ImmutableArray<Diagnostic> diagnostics);

        if (!diagnostics.IsEmpty)
        {
            throw new Exception(string.Join(Environment.NewLine, diagnostics));
        }

        return updatedCompilation;
    }

    private static CSharpCompilation CreateCompilation(string source, string testName)
    {
        CSharpParseOptions options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Latest);
        SyntaxTree sourceTree = CSharpSyntaxTree.ParseText(source, options);
        ImmutableArray<MetadataReference> references = GeneratorTestBase.DependencyAssemblies.Value;

        return CSharpCompilation.Create(
            testName,
            [sourceTree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
    }
}