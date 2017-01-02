namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System;

    [JsonObject(MemberSerialization.Fields)]
    public class ContentPart : BlobPartBase, IReadOnlyContentPart
    {
        public byte[] Data { get; set; }

        public static readonly IReadOnlyContentPart Empty = new ContentPart
        {
            Data = new byte[]{},
        };

        internal override string Name { get { return Content.ContentName; } }
    }
}