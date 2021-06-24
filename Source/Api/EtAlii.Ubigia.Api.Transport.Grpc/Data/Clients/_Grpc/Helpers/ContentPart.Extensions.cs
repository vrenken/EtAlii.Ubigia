// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;
    using Google.Protobuf;

    public static class ContentPartExtension
    {
        public static WireProtocol.ContentPart ToWire(this ContentPart contentPart)
        {
            return new()
            {
                Id = contentPart.Id,
                Data = ByteString.CopyFrom(contentPart.Data),
                Stored = contentPart.Stored,
            };

        }

        public static IEnumerable<WireProtocol.ContentPart> ToWire(this IEnumerable<ContentPart> contentParts)
        {
            return contentParts.Select(s => s.ToWire());
        }

        public static ContentPart ToLocal(this WireProtocol.ContentPart contentPart)
        {
            var result = new ContentPart { Data = contentPart.Data.ToByteArray(), Id = contentPart.Id};
            BlobPart.SetStored(result, contentPart.Stored);
            return result;
        }

        public static IEnumerable<ContentPart> ToLocal(this IEnumerable<WireProtocol.ContentPart> contentParts)
        {
            return contentParts.Select(s => s.ToLocal());
        }
    }
}
