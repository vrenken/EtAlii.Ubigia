namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ContentDefinitionExtension
    {
        public static ContentDefinition ToLocal(this WireProtocol.ContentDefinition contentDefinition)
        {
            var result = new ContentDefinition
            {
//                Name = contentDefinition.Name;
                Stored = contentDefinition.Stored,
                Summary = contentDefinition.Summary?.ToLocal(),
                TotalParts = contentDefinition.TotalParts,               
                Checksum = contentDefinition.Checksum,
                
                Size = contentDefinition.Size,
            };
            foreach (var part in contentDefinition.Parts)
            {
                result.Parts.Add(part.ToLocal());
            }
                
            return result;
        }

        public static WireProtocol.ContentDefinition ToWire(this ContentDefinition contentDefinition)
        {
            var result = new WireProtocol.ContentDefinition
            {
//                Name = contentDefinition.Name,
                Stored = contentDefinition.Stored,
                Summary = contentDefinition.Summary?.ToWire(),
                TotalParts = contentDefinition.TotalParts,
                Checksum = contentDefinition.Checksum,
                Size = contentDefinition.Size,
            };
            result.Parts.AddRange(contentDefinition.Parts.ToWire());

            return result;
        }

        public static IEnumerable<WireProtocol.ContentDefinition> ToWire(this IEnumerable<ContentDefinition> contentDefinitions)
        {
            return contentDefinitions.Select(s => s.ToWire());
        }
    }
}
