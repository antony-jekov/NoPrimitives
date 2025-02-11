using System;
using System.Reflection;


namespace NoPrimitives.OutputGenerators;

internal static class References
{
    internal static readonly Lazy<Assembly> Assembly = new(() => typeof(References).Assembly);
}