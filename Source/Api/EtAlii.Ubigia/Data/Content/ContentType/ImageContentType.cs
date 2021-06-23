// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class ImageContentType : ContentType
    {
        private const string _imageContentTypeId = "Image";

        public ContentType PortableNetworkGraphics { get; } = new(_imageContentTypeId, "Png");

        public ContentType Gif { get; } = new(_imageContentTypeId, "Gif");

        public ContentType Jpeg { get; } = new(_imageContentTypeId, "Jpeg");

        internal ImageContentType()
            : base(_imageContentTypeId)
        {
        }
    }
}