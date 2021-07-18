// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    [Flags]
    public enum EntryRelations : byte
    {
        None = 0,
        Parent = 1,
        Child = 2,
        Previous = 4,
        Next = 8,
        Downdate = 16,
        Update = 32,
        Index = 64,
        Indexed = 128,
        All = byte.MaxValue,
    }
}
