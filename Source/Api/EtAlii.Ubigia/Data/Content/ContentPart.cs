namespace EtAlii.Ubigia
{
    using System;

    public class ContentPart : BlobPart
    {
        public byte[] Data { get; init; }

        public static readonly ContentPart Empty = new()
        {
            Data = Array.Empty<byte>(),
        };

        protected override string Name => Content.ContentName;
    }
}
