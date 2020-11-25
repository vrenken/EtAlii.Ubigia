namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;

    public sealed partial class ContentDefinition : BlobBase, IEquatable<ContentDefinition>, IReadOnlyContentDefinition
    {
        internal const string ContentDefinitionName = "ContentDefinition";

        public ulong Size { get; set; }

        public ulong Checksum { get; set; }

        public IList<ContentDefinitionPart> Parts { get; } = new List<ContentDefinitionPart>();

        IEnumerable<IReadOnlyContentDefinitionPart> IReadOnlyContentDefinition.Parts => Parts;

        public static readonly IReadOnlyContentDefinition Empty = new ContentDefinition
        {
            Checksum = 0,
            Size = 0,
        };

        protected internal override string Name => ContentDefinitionName; 
    }
}