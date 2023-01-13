// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

public sealed class Value
{
    public string Name { get; }
    public object Object { get; }

    public Value(string name, object @object)
    {
        Name = name;
        Object = @object;
    }

    public override string ToString()
    {
        return $"{Name}: {Object ?? "NULL"}";
    }
}
