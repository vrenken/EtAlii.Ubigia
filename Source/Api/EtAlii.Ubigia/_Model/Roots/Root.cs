// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.IO;

/// <summary>
/// Represents a root in a <see cref="Space"/>.
/// Roots are used to start entity traversals from.
/// </summary>
public sealed class Root : IIdentifiable, IBinarySerializable
{
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <summary>
    /// The type of the root.
    /// </summary>
    public RootType Type { get; set; }

    /// <summary>
    /// The current (=last) Ubigia identifier from which traversal can commence.
    /// </summary>
    public Identifier Identifier { get; set; }

    public void Write(BinaryWriter writer)
    {
        writer.Write(Id);
        writer.Write(Name.ToUpper()); // RCI2022: We want to make roots case insensitive.
        writer.Write(Type.Value);
        Identifier.Write(writer, Identifier);
    }

    public void Read(BinaryReader reader)
    {
        Id = reader.Read<Guid>();
        Name = reader.ReadString();
        Type = new RootType(reader.ReadString());
        Identifier = reader.Read<Identifier>();
    }
}
