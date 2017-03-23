namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public sealed partial class ContentDefinition : BlobBase, IEquatable<ContentDefinition>, IReadOnlyContentDefinition
    {
        internal const string ContentDefinitionName = "ContentDefinition";

        public UInt64 Size { get; set; }
        public UInt64 Checksum { get; set; }

        public IList<ContentDefinitionPart> Parts { get; } = new List<ContentDefinitionPart>();

        IEnumerable<IReadOnlyContentDefinitionPart> IReadOnlyContentDefinition.Parts => Parts.Cast<IReadOnlyContentDefinitionPart>();

        public static readonly IReadOnlyContentDefinition Empty = new ContentDefinition
        {
            Checksum = 0,
            Size = 0,
        };

        protected internal override string Name => ContentDefinitionName;
    }
}