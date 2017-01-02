namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [JsonObject(MemberSerialization.Fields)]
    public sealed partial class ContentDefinition : BlobBase, IEquatable<ContentDefinition>, IReadOnlyContentDefinition
    {
        internal const string ContentDefinitionName = "ContentDefinition";

        public UInt64 Size { get; set; }
        public UInt64 Checksum { get; set; }

        public IList<ContentDefinitionPart> Parts { get { return _parts; } }

        private readonly IList<ContentDefinitionPart> _parts = new List<ContentDefinitionPart>();
        IEnumerable<IReadOnlyContentDefinitionPart> IReadOnlyContentDefinition.Parts { get { return this.Parts.Cast<IReadOnlyContentDefinitionPart>(); } }

        public static readonly IReadOnlyContentDefinition Empty = new ContentDefinition
        {
            Checksum = 0,
            Size = 0,
        };

        internal override string Name { get { return ContentDefinitionName; } }
    }
}