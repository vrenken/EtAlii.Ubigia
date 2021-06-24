// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    [Flags]
    public enum EntryContent : byte
    {
        Definition = 1,
        PartDefinition = 2,
        Part = 3,
    }
}
