using System;


namespace NoPrimitives;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ValueObjectAttribute<TPrimitive>() : ValueObjectAttribute(typeof(TPrimitive));

#pragma warning disable CS9113 // Parameter is unread.

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ValueObjectAttribute(Type primitiveType) : Attribute;

#pragma warning restore CS9113 // Parameter is unread.