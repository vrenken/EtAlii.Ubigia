// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.IO;

public partial struct Identifier
{
    public static void Write(BinaryWriter writer, Identifier item)
    {
        writer.Write(item.Storage);
        writer.Write(item.Account);
        writer.Write(item.Space);

        writer.Write(item.Era);
        writer.Write(item.Period);
        writer.Write(item.Moment);
    }

    public static Identifier Read(BinaryReader reader)
    {
        var storage = reader.Read<Guid>();
        var account = reader.Read<Guid>();
        var space = reader.Read<Guid>();

        var era = reader.ReadUInt64();
        var period = reader.ReadUInt64();
        var moment = reader.ReadUInt64();
        return Create(storage, account, space, era, period, moment);
    }
}
