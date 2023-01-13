// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.IO;

/// <summary>
/// Represents a single space in a <see cref="Storage"/>.
/// </summary>
public sealed class Space : IIdentifiable, IBinarySerializable
{
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <summary>
    /// The account to which this Space belongs.
    /// </summary>
    public Guid AccountId { get; set; }

    public void Write(BinaryWriter writer)
    {
        writer.Write(Id);
        writer.Write(Name);
        writer.Write(AccountId);
    }

    public void Read(BinaryReader reader)
    {
        Id = reader.Read<Guid>();
        Name = reader.ReadString();
        AccountId = reader.Read<Guid>();
    }
}
