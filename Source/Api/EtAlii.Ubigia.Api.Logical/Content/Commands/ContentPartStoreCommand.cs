// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.IO;

    public class ContentPartStoreCommand
    {
        public readonly Stream Stream;
        public readonly ulong SizeInBytes;
        public readonly ulong RequiredParts;
        public readonly ulong PartSize;
        public readonly Identifier Identifier;
        public readonly ContentDefinition ContentDefinition;
        public readonly Content Content;

        public ContentPartStoreCommand(
            Stream stream,
            ulong sizeInBytes,
            ulong requiredParts,
            ulong partSize,
            Identifier identifier,
            ContentDefinition contentDefinition,
            Content content)
        {
            Stream = stream;
            SizeInBytes = sizeInBytes;
            RequiredParts = requiredParts;
            PartSize = partSize;
            Identifier = identifier;
            ContentDefinition = contentDefinition;
            Content = content;
        }
    }
}
