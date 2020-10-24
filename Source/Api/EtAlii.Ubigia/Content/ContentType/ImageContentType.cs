namespace EtAlii.Ubigia
{
    public class ImageContentType : ContentType
    {
        private const string ImageContentTypeId = "Image";

        public ContentType PortableNetworkGraphics { get; } = new ContentType(ImageContentTypeId, "Png");

        public ContentType Gif { get; } = new ContentType(ImageContentTypeId, "Gif");

        public ContentType Jpeg { get; } = new ContentType(ImageContentTypeId, "Jpeg");

        internal ImageContentType()
            : base(ImageContentTypeId)
        {
        }
    }
}