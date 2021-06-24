// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class BlobSummaryExtension
    {
        public static BlobSummary ToLocal(this WireProtocol.BlobSummary blobSummary)
        {
            var availableParts = blobSummary.AvailableParts
                .ToArray();
            return new BlobSummary 
            {
                IsComplete = blobSummary.IsComplete, 
                TotalParts = blobSummary.TotalParts, 
                AvailableParts = availableParts
            };
        }

        public static WireProtocol.BlobSummary ToWire(this BlobSummary blobSummary)
        {
            var result = new WireProtocol.BlobSummary
            {
                IsComplete = blobSummary.IsComplete,
                TotalParts = blobSummary.TotalParts,
            };
            result.AvailableParts.AddRange(blobSummary.AvailableParts);
            return result;
        }

        public static IEnumerable<WireProtocol.BlobSummary> ToWire(this IEnumerable<BlobSummary> blobSummarys)
        {
            return blobSummarys.Select(s => s.ToWire());
        }
    }
}
