// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.IO;

    public partial struct Relation
    {
        public void Write(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Moment);
        }

        public void Read(BinaryReader reader)
        {
            _id = reader.Read<Identifier>();
            _moment = reader.ReadUInt64();
        }
    }
}
