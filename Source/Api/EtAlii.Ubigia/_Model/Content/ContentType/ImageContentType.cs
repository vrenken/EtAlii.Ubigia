// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public sealed class ImageContentType : ContentType
    {
        private const string ImageContentTypeId = "Image";

        public ContentType PortableNetworkGraphics { get; } = new(ImageContentTypeId, "Png");

        public ContentType Gif { get; } = new(ImageContentTypeId, "Gif");

        public ContentType Jpeg { get; } = new(ImageContentTypeId, "Jpeg");

        internal ImageContentType()
            : base(ImageContentTypeId)
        {
        }
    }
}
