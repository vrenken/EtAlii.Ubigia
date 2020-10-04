namespace EtAlii.Ubigia
{
    public class ContentPart : BlobPartBase, IReadOnlyContentPart
    {
        public byte[] Data { get; set; }

        public static readonly IReadOnlyContentPart Empty = new ContentPart
        {
            Data = new byte[]{},
        };

        protected internal override string Name => Content.ContentName;
    }
}