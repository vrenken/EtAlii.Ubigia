// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.IO;

    public class AuthenticationToken
    {
        public long Salt { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public static void Write(BinaryWriter writer, AuthenticationToken item)
        {
            writer.Write(item.Salt);
            writer.Write(item.Name);
            writer.Write(item.Address);
        }

        public static AuthenticationToken Read(BinaryReader reader)
        {
            return new AuthenticationToken
            {
                Salt = reader.ReadInt64(),
                Name = reader.ReadString(),
                Address = reader.ReadString()
            };
        }
    }
}
