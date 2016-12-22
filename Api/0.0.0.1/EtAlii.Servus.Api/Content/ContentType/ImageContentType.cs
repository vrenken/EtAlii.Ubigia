namespace EtAlii.Servus.Api
{
    public class ImageContentType : ContentType
    {
        private const string _imageContentTypeId = "Image";

        public ContentType PortableNetworkGraphics { get { return _portableNetworkGraphics; } }
        private readonly ContentType _portableNetworkGraphics = new ContentType(_imageContentTypeId, "Png");

        public ContentType Gif { get { return _gif; } }
        private readonly ContentType _gif = new ContentType(_imageContentTypeId, "Gif");

        public ContentType Jpeg { get { return _jpeg; } }
        private readonly ContentType _jpeg = new ContentType(_imageContentTypeId, "Jpeg");

        internal ImageContentType()
            : base(_imageContentTypeId)
        {
        }
    }
}