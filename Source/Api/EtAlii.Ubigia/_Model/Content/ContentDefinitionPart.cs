// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System.IO;

public sealed class ContentDefinitionPart : BlobPart
{
    internal ContentDefinitionPart()
    {
    }

    public ulong Checksum { get; private set; }
    public ulong Size { get; private set; }

    public static readonly ContentDefinitionPart Empty = new() { Checksum = 0, Size = 0 };

    /// <inheritdoc />
    protected override string Name => ContentDefinition.ContentDefinitionName;

    public static ContentDefinitionPart Create(ulong id, ulong checksum, ulong size)
    {
        return new ContentDefinitionPart
        {
            Id = id,
            Checksum = checksum,
            Size = size
        };
    }
    public override void Write(BinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(Checksum);
        writer.Write(Size);
    }

    public override void Read(BinaryReader reader)
    {
        base.Read(reader);
        Checksum = reader.ReadUInt64();
        Size = reader.ReadUInt64();
    }
}
