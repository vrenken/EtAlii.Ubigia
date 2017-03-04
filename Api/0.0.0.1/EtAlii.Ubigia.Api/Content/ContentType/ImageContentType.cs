namespace EtAlii.Ubigia.Api
{
    public class ImageContentType : ContentType
    {
        private const string _imageContentTypeId = "Image";

        public ContentType PortableNetworkGraphics => _portableNetworkGraphics;
        private readonly ContentType _portableNetworkGraphics = new ContentType(_imageContentTypeId, "Png");

        public ContentType Gif => _gif;
        private readonly ContentType _gif = new ContentType(_imageContentTypeId, "Gif");

        public ContentType Jpeg => _jpeg;
        private readonly ContentType _jpeg = new ContentType(_imageContentTypeId, "Jpeg");

        internal ImageContentType()
            : base(_imageContentTypeId)
        {
        }
    }
}