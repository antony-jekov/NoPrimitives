using System;


namespace NoPrimitives;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ValueObjectAttribute<TPrimitive>(Integrations integrations = Integrations.NotSpecified)
    : ValueObjectAttribute(typeof(TPrimitive), integrations);

#pragma warning disable CS9113 // Parameter is unread.

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ValueObjectAttribute(Type primitiveType, Integrations integrations = Integrations.NotSpecified)
    : Attribute;

#pragma warning restore CS9113 // Parameter is unread.