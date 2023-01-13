// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory;

public class File : Item
{
    public byte[] Content { get; set; }

    public File(string name)
        : base(name)
    {
    }

    public override string ToString()
    {
        return Name ?? "[Empty]";
    }
}
