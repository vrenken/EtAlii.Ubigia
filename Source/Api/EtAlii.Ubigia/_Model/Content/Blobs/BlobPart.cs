// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System.IO;

public abstract partial class BlobPart : IBinarySerializable
{
    /// <summary>
    /// Returns true when the blob part has been stored.
    /// </summary>
    public bool Stored { get; private set; }

    /// <summary>
    /// Returns the type name of the blob part.
    /// </summary>
    protected abstract string Name { get; }

    public ulong Id { get; protected set; }

    public virtual void Write(BinaryWriter writer)
    {
        writer.Write(Id);
    }

    public virtual void Read(BinaryReader reader)
    {
        Id = reader.ReadUInt64();
    }
}
