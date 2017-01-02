namespace EtAlii.Ubigia.Api
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public class ContentPart : BlobPartBase, IReadOnlyContentPart
    {
        public byte[] Data { get; set; }

        public static readonly IReadOnlyContentPart Empty = new ContentPart
        {
            Data = new byte[]{},
        };

        protected internal override string Name { get { return Content.ContentName; } }
    }
}