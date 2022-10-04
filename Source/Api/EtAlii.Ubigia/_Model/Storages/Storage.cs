// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.IO;

    /// <summary>
    /// A Ubigia storage, including it's address where it can be found.
    /// </summary>
    public sealed class Storage : IIdentifiable, IBinarySerializable
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }
        public string Address { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Name);
            writer.Write(Address);
        }

        public void Read(BinaryReader reader)
        {
            Id = reader.Read<Guid>();
            Name = reader.ReadString();
            Address = reader.ReadString();
        }
    }
}
