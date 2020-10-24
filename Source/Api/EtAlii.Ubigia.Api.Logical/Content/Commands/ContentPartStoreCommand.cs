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
        public readonly IReadOnlyContentDefinition ContentDefinition;
        public readonly IReadOnlyContent Content;

        public ContentPartStoreCommand(
            Stream stream,
            ulong sizeInBytes,
            ulong requiredParts,
            ulong partSize,
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
