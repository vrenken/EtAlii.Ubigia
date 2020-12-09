﻿namespace EtAlii.Ubigia
{
    using System;

    public class ContentPart : BlobPartBase
    {
        public byte[] Data { get; init; }

        public static readonly ContentPart Empty = new() 
        {
            Data = Array.Empty<byte>(),
        };

        protected internal override string Name => Content.ContentName;
    }
}