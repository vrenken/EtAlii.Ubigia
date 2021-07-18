// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    public class ContentPart : BlobPart
    {
        public byte[] Data { get; init; }

        public static readonly ContentPart Empty = new()
        {
            Data = Array.Empty<byte>(),
        };

        protected override string Name => Content.ContentName;
    }
}
