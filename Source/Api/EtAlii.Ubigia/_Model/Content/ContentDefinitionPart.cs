// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed class ContentDefinitionPart : BlobPart
    {
        public ulong Checksum { get; init; }
        public ulong Size { get; init; }

        public static readonly ContentDefinitionPart Empty = new() { Checksum = 0, Size = 0 };

        /// <inheritdoc />
        protected override string Name => ContentDefinition.ContentDefinitionName;
    }
}
