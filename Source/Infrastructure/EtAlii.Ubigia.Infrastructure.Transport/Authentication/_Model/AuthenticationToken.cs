// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.IO;

    public class AuthenticationToken : IBinarySerializable
    {
        public long Salt { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Salt);
            writer.Write(Name);
            writer.Write(Address);
        }

        public void Read(BinaryReader reader)
        {
            Salt = reader.ReadInt64();
            Name = reader.ReadString();
            Address = reader.ReadString();
        }
    }
}
