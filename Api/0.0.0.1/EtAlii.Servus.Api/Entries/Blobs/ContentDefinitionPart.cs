namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System;

    [JsonObject(MemberSerialization.Fields)]
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

        internal override string Name { get { return ContentDefinition.ContentDefinitionName; } }
    }
}