// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;

public sealed partial class Entry
{
    public static Entry NewEntry(Guid storage, Guid account, Guid space)
    {
        var identifier = Identifier.NewIdentifier(storage, account, space);
        return new Entry(identifier);
    }

    public static Entry NewEntry(in Identifier id, Relation previous)
    {
        return new(id, previous);
    }

    public static Entry NewEntry(in Identifier id)
    {
        return new(id);
    }

    /// <summary>
    /// Instantiate a new entry.
    /// </summary>
    /// <returns></returns>
    public static Entry NewEntry()
    {
        return new();
    }

}
