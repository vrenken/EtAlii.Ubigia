﻿namespace EtAlii.Ubigia.Api
{
    using System;

    public interface IBlob
    {
        bool Stored { get; }
        UInt64 TotalParts { get; set; }

        BlobSummary Summary { get; }
    }
}
