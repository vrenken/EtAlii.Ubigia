namespace EtAlii.Ubigia
{
    using System;

    public class ContentPart : BlobPartBase, IReadOnlyContentPart
    {
        public byte[] Data { get; set; }

        public static readonly IReadOnlyContentPart Empty = new ContentPart
        {
            Data = Array.Empty<byte>(),
        };

        protected internal override string Name => Content.ContentName;
    }
}