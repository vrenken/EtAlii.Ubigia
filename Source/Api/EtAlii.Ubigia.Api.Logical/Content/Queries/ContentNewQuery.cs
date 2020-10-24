namespace EtAlii.Ubigia.Api.Logical
{
    public class ContentNewQuery
    {
        public readonly Identifier Identifier;
        public readonly ulong SizeInBytes;
        public readonly ulong RequiredParts;
        public readonly ulong PartSize;

        public ContentNewQuery(Identifier identifier, ulong sizeInBytes, ulong requiredParts, ulong partSize)
        {
            Identifier = identifier;
            SizeInBytes = sizeInBytes;
            RequiredParts = requiredParts;
            PartSize = partSize;
        }
    }
}
