// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System;
using System.IO;

public class SimpleTestItem : IBinarySerializable
{
    public string Name { get; set; }
    public Guid Value { get; set; }

    public void Write(BinaryWriter writer)
    {
        writer.Write(Name);
        writer.Write(Value);
    }

    public void Read(BinaryReader reader)
    {
        Name = reader.ReadString();
        Value = reader.Read<Guid>();
    }
}
