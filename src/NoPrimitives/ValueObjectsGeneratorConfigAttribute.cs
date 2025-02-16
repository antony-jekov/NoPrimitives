using System;


namespace NoPrimitives;

#pragma warning disable CS9113 // Parameter is unread.

[AttributeUsage(AttributeTargets.Assembly)]
public class ValueObjectsGeneratorConfigAttribute(Integrations integrations = Integrations.NotSpecified) : Attribute
{
}

#pragma warning restore CS9113 // Parameter is unread.