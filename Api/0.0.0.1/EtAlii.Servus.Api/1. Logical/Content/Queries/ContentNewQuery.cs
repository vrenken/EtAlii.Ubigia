namespace EtAlii.Servus.Api.Logical
{
    using System;

    public class ContentNewQuery
    {
        public readonly Identifier Identifier;
        public readonly UInt64 SizeInBytes;
        public readonly UInt64 RequiredParts;
        public readonly UInt64 PartSize;

        public ContentNewQuery(Identifier identifier, UInt64 sizeInBytes, UInt64 requiredParts, UInt64 partSize)
        {
            Identifier = identifier;
            SizeInBytes = sizeInBytes;
            RequiredParts = requiredParts;
            PartSize = partSize;
        }
    }
}
