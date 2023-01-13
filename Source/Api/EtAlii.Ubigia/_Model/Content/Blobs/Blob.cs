// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System.IO;

public abstract partial class Blob : IBinarySerializable
{
    /// <summary>
    /// Returns true when the blob has been stored.
    /// </summary>
    public bool Stored { get; protected set; }

    /// <summary>
    /// Returns the type name of the blob part.
    /// </summary>
    protected abstract string Name { get; }

    public BlobSummary Summary { get; protected set; }

    /// <summary>
    /// Returns the total number of parts that make up the blob.
    /// </summary>
    public ulong TotalParts { get; protected set; }

    public virtual void Write(BinaryWriter writer)
    {
        writer.WriteOptional(Summary);
        writer.Write(TotalParts);
    }

    public virtual void Read(BinaryReader reader)
    {
        Summary = reader.ReadOptionalReferenceType<BlobSummary>();
        TotalParts = reader.ReadUInt64();
    }
}
