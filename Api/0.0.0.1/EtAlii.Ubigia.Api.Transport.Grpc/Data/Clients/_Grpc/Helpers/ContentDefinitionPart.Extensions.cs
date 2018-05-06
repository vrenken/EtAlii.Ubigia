
namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ContentDefinitionPartExtension
    {
        public static ContentDefinitionPart ToLocal(this WireProtocol.ContentDefinitionPart contentDefinitionPart)
        {
            return new ContentDefinitionPart
            {
                Id = contentDefinitionPart.Id,
                Checksum = contentDefinitionPart.Checksum,
                Size = contentDefinitionPart.Size,
                Stored = contentDefinitionPart.Stored,
            };
        }

        public static WireProtocol.ContentDefinitionPart ToWire(this ContentDefinitionPart contentDefinitionPart)
        {
            return new WireProtocol.ContentDefinitionPart
            {
                Id = contentDefinitionPart.Id,
                Checksum = contentDefinitionPart.Checksum,
                Size = contentDefinitionPart.Size,
                Stored = contentDefinitionPart.Stored,
            };
            
        }

        public static IEnumerable<WireProtocol.ContentDefinitionPart> ToWire(this IEnumerable<ContentDefinitionPart> contentDefinitionParts)
        {
            return contentDefinitionParts.Select(s => s.ToWire());
        }
    }
}
