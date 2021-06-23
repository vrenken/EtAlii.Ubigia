// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class ContentDefinition : Blob, IEquatable<ContentDefinition>
    {
        internal const string ContentDefinitionName = "ContentDefinition";

        public ulong Size { get; init; }

        public ulong Checksum { get; init; }

        public ContentDefinitionPart[] Parts { get; init; } = Array.Empty<ContentDefinitionPart>();

        protected override string Name => ContentDefinitionName;

        public ContentDefinition ExceptParts()
        {
            return new() 
            {
                // Blob
                Stored = Stored,
                Summary = Summary,
                TotalParts = TotalParts,

                // ContentDefinition
                Size = Size,
                Checksum = Checksum,
            };
        }
        
        public ContentDefinition WithPart(IEnumerable<ContentDefinitionPart> contentDefinitionParts)
        {
            return new() 
            {
                // Blob
                Stored = Stored,
                Summary = Summary,
                TotalParts = TotalParts,

                // ContentDefinition
                Size = Size,
                Checksum = Checksum,
                Parts = Parts.Concat(contentDefinitionParts).ToArray()
            };
        }
    }
}