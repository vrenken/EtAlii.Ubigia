﻿namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ContentDefinitionExtension
    {
        public static ContentDefinition ToLocal(this WireProtocol.ContentDefinition contentDefinition)
        {
            ContentDefinition result = null;
            if (contentDefinition != null)
            { 
                result = new ContentDefinition
                {
                    Checksum = contentDefinition.Checksum,
                    
                    Size = contentDefinition.Size,
                    Parts = contentDefinition.Parts.Select(p => p.ToLocal()).ToArray()
                };
                Blob.SetTotalParts(result, contentDefinition.TotalParts);
                Blob.SetStored(result, contentDefinition.Stored);
                Blob.SetSummary(result, contentDefinition.Summary?.ToLocal());
            }
            return result;
        }

        public static WireProtocol.ContentDefinition ToWire(this ContentDefinition contentDefinition)
        {
            WireProtocol.ContentDefinition result = null;
            if (contentDefinition != null)
            {
                result = new WireProtocol.ContentDefinition
                {
                    Stored = contentDefinition.Stored,
                    Summary = contentDefinition.Summary?.ToWire(),
                    TotalParts = contentDefinition.TotalParts,
                    Checksum = contentDefinition.Checksum,
                    Size = contentDefinition.Size,
                };
                result.Parts.AddRange(contentDefinition.Parts.ToWire());
            }

            return result;
        }

        public static IEnumerable<WireProtocol.ContentDefinition> ToWire(this IEnumerable<ContentDefinition> contentDefinitions)
        {
            return contentDefinitions.Select(s => s.ToWire());
        }
    }
}
