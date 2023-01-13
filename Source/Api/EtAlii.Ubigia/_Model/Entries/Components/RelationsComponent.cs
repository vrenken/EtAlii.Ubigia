// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.IO;

public abstract class RelationsComponent : CompositeComponent
{
    public Relation[] Relations { get; internal set; } = Array.Empty<Relation>();


    public override void Write(BinaryWriter writer)
    {
        writer.WriteMany(Relations);
    }

    public override void Read(BinaryReader reader)
    {
        Relations = reader.ReadMany<Relation>();
    }
}
