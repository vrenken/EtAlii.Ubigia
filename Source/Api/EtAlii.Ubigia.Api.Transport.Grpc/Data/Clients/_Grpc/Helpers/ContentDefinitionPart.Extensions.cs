// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ContentDefinitionPartExtension
    {
        public static ContentDefinitionPart ToLocal(this WireProtocol.ContentDefinitionPart contentDefinitionPart)
        {
            var result = ContentDefinitionPart.Create(contentDefinitionPart.Id, contentDefinitionPart.Checksum, contentDefinitionPart.Size);
            BlobPart.SetStored(result, contentDefinitionPart.Stored);
            return result;
        }

        public static WireProtocol.ContentDefinitionPart ToWire(this ContentDefinitionPart contentDefinitionPart)
        {
            return new()
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
