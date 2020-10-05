namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;

    public sealed partial class ContentDefinition : BlobBase, IEquatable<ContentDefinition>, IReadOnlyContentDefinition
    {
        internal const string ContentDefinitionName = "ContentDefinition";

        public ulong Size { get => _size; set => _size = value; }
        private ulong _size;
        
        public ulong Checksum { get => _checksum; set => _checksum = value; }
        private ulong _checksum;

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