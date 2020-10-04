namespace EtAlii.Ubigia
{
    public class ContentDefinitionPart : BlobPartBase, IReadOnlyContentDefinitionPart
    {
        public ulong Checksum { get; set; }
        public ulong Size { get; set; }

        public static readonly IReadOnlyContentDefinitionPart Empty = new ContentDefinitionPart
        {
            Id = 0,
            Checksum = 0,
            Size = 0,
        };

        protected internal override string Name => ContentDefinition.ContentDefinitionName;
    }
}