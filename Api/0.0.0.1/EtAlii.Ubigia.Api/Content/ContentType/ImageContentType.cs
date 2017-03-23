namespace EtAlii.Ubigia.Api
{
    public class ImageContentType : ContentType
    {
        private const string _imageContentTypeId = "Image";

        public ContentType PortableNetworkGraphics { get; } = new ContentType(_imageContentTypeId, "Png");

        public ContentType Gif { get; } = new ContentType(_imageContentTypeId, "Gif");

        public ContentType Jpeg { get; } = new ContentType(_imageContentTypeId, "Jpeg");

        internal ImageContentType()
            : base(_imageContentTypeId)
        {
        }
    }
}