using System;


namespace NoPrimitives.Core;

[Flags]
public enum Integrations
{
    None = 0,
    SystemTextJson = 1,
    TypeConversions = 1 << 1,
    NewtonsoftJson = 1 << 2,
    Default = Integrations.SystemTextJson | Integrations.TypeConversions,
}