namespace EtAlii.Ubigia
{
    using System;
    using EtAlii.Ubigia.Api;

    public class ContentDefinitionPart : BlobPartBase, IReadOnlyContentDefinitionPart
    {
        public UInt64 Checksum { get; set; }
        public UInt64 Size { get; set; }

        public static readonly IReadOnlyContentDefinitionPart Empty = new ContentDefinitionPart
        {
            Id = 0,
            Checksum = 0,
            Size = 0,
        };

        protected internal override string Name => ContentDefinition.ContentDefinitionName;
    }
}