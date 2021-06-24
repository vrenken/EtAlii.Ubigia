// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    public static class TagComponentExtensions
    {
        public static TagComponent ToLocal(this WireProtocol.TagComponent tagComponent)
        {
            var result = new TagComponent
            {
                Stored = tagComponent.Stored,
                Tag = tagComponent.Tag
            };

            return result;
        }

        public static WireProtocol.TagComponent ToWire(this TagComponent tagComponent)
        {
            var result = new WireProtocol.TagComponent
            {
                Stored = tagComponent.Stored,
            };

            if (tagComponent.Tag != null)
            {
                result.Tag = tagComponent.Tag;
            }

            return result;
        }
    }
}
