namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;
    using Google.Protobuf;

    public static class ContentPartExtension
    {
        public static ContentPart ToLocal(this WireProtocol.ContentPart contentPart)
        {
            return new ContentPart
            {
                Id = contentPart.Id,
                Data = contentPart.Data.ToByteArray(),
                Stored = contentPart.Stored,
            };
        }

        public static WireProtocol.ContentPart ToWire(this ContentPart contentPart)
        {
            return new WireProtocol.ContentPart
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
        public static IEnumerable<ContentPart> ToLocal(this IEnumerable<WireProtocol.ContentPart> contentParts)
        {
            return contentParts.Select(s => s.ToLocal());
        }

    }
}
