// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System.IO;

public abstract class RelationComponent : NonCompositeComponent
{
    public Relation Relation { get; internal set; }

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Relation);
    }

    public override void Read(BinaryReader reader)
    {
        Relation = reader.Read<Relation>();
    }
}
