namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.IO;

    public class ContentPartStoreCommand
    {
        public readonly Stream Stream;
        public readonly UInt64 SizeInBytes;
        public readonly UInt64 RequiredParts;
        public readonly UInt64 PartSize;
        public readonly Identifier Identifier;
        public readonly IReadOnlyContentDefinition ContentDefinition;
        public readonly IReadOnlyContent Content;

        public ContentPartStoreCommand(
            Stream stream,
            UInt64 sizeInBytes,
            UInt64 requiredParts,
            UInt64 partSize,
            Identifier identifier,
            IReadOnlyContentDefinition contentDefinition,
            IReadOnlyContent content)
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
