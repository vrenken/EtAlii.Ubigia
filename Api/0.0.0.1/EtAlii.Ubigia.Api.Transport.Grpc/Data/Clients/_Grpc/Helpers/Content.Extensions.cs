namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ContentExtension
    {
        public static Content ToLocal(this WireProtocol.Content content)
        {
            return new Content
            {
                Stored = content.Stored,
                Summary = content.Summary?.ToLocal(),
                TotalParts = content.TotalParts,               
            };
        }

        public static WireProtocol.Content ToWire(this Content content)
        {
            return new WireProtocol.Content
            {
                Stored = content.Stored,
                Summary = content.Summary?.ToWire(),
                TotalParts = content.TotalParts,
            };
        }

        public static IEnumerable<WireProtocol.Content> ToWire(this IEnumerable<Content> contents)
        {
            return contents.Select(s => s.ToWire());
        }
    }
}
