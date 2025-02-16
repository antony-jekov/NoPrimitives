using System;


namespace NoPrimitives;

[Flags]
public enum Integrations
{
    NotSpecified = 0,
    None = 1,
    SystemTextJson = 1 << 1,
    TypeConversions = 1 << 2,
    NewtonsoftJson = 1 << 3,
    Default = Integrations.SystemTextJson | Integrations.TypeConversions,
}