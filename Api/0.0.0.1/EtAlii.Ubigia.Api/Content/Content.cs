﻿namespace EtAlii.Ubigia.Api
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public class Content : BlobBase, IReadOnlyContent
    {
        internal const string ContentName = "Content";

        //public IList<ContentPart> Parts { get { return _parts; } }

        //private readonly IList<ContentPart> _parts = new List<ContentPart>();
        //IEnumerable<IReadOnlyContentPart> IReadOnlyContent.Parts { get { return this.Parts.Cast<IReadOnlyContentPart>(); } }

        protected internal override string Name { get { return ContentName; } }
    }
}