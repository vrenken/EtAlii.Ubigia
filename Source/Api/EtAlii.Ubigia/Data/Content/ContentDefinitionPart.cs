namespace EtAlii.Ubigia
{
    public class ContentDefinitionPart : BlobPart
    {
        public ulong Checksum { get; init; }
        public ulong Size { get; init; }

        public static readonly ContentDefinitionPart Empty = new() { Checksum = 0, Size = 0 };

        protected override string Name => ContentDefinition.ContentDefinitionName;
    }
}