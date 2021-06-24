// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ContentExtension
    {
        public static Content ToLocal(this WireProtocol.Content content)
        {
            if (content == null)
            {
                return null;
            }
            var result = new Content();
            Blob.SetTotalParts(result, content.TotalParts);
            Blob.SetStored(result, content.Stored);
            Blob.SetSummary(result, content.Summary?.ToLocal());
            return result;
        }

        public static WireProtocol.Content ToWire(this Content content)
        {
            return content == null ? null : new WireProtocol.Content
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
