// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.IO;

public sealed class ContentPart : BlobPart
{
    public byte[] Data { get; private set; }

    public static readonly ContentPart Empty = new()
    {
        Data = Array.Empty<byte>(),
    };

    /// <inheritdoc />
    protected override string Name => Content.ContentName;

    public static ContentPart Create(ulong id, byte[] data)
    {
        return new ContentPart { Id = id, Data = data };
    }

    public override void Write(BinaryWriter writer)
    {
        base.Write(writer);
        writer.WriteMany(Data);
    }

    public override void Read(BinaryReader reader)
    {
        base.Read(reader);
        Data = reader.ReadMany<byte>();
    }
}
