// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    public class ContentDefinitionQuery
    {
        public readonly Identifier Identifier;
        public readonly ulong SizeInBytes;
        public readonly ulong RequiredParts;
        public readonly ulong PartSize;

        public ContentDefinitionQuery(in Identifier identifier, ulong sizeInBytes, ulong requiredParts, ulong partSize)
        {
            Identifier = identifier;
            SizeInBytes = sizeInBytes;
            RequiredParts = requiredParts;
            PartSize = partSize;
        }
    }
}
