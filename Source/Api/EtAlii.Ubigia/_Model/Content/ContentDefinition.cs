// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public sealed partial class ContentDefinition : Blob, IEquatable<ContentDefinition>
    {
        internal const string ContentDefinitionName = "ContentDefinition";

        public ulong Size { get; private set; }

        public ulong Checksum { get; private set; }

        public ContentDefinitionPart[] Parts { get; private set; } = Array.Empty<ContentDefinitionPart>();

        /// <inheritdoc />
        protected override string Name => ContentDefinitionName;

        public ContentDefinition ExceptParts()
        {
            return new()
            {
                // Blob
                Stored = Stored,
                Summary = Summary,
                TotalParts = TotalParts,

                // ContentDefinition
                Size = Size,
                Checksum = Checksum,
            };
        }

        public ContentDefinition WithPart(IEnumerable<ContentDefinitionPart> contentDefinitionParts)
        {
            return new()
            {
                // Blob
                Stored = Stored,
                Summary = Summary,
                TotalParts = TotalParts,

                // ContentDefinition
                Size = Size,
                Checksum = Checksum,
                Parts = Parts.Concat(contentDefinitionParts).ToArray()
            };
        }

        public static ContentDefinition Create(ulong checksum, ulong size, ContentDefinitionPart[] parts)
        {
            return new ContentDefinition
            {
                Checksum = checksum,
                Size = size,
                Parts = parts
            };
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(Checksum);
            writer.Write(Size);
            writer.WriteMany(Parts);
        }

        public override void Read(BinaryReader reader)
        {
            base.Read(reader);
            Checksum = reader.ReadUInt64();
            Size = reader.ReadUInt64();
            Parts = reader.ReadMany<ContentDefinitionPart>();
        }
    }
}
