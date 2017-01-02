namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    [JsonObject(MemberSerialization.Fields)]
    public class Content : BlobBase, IReadOnlyContent
    {
        internal const string ContentName = "Content";

        //public IList<ContentPart> Parts { get { return _parts; } }

        //private readonly IList<ContentPart> _parts = new List<ContentPart>();
        //IEnumerable<IReadOnlyContentPart> IReadOnlyContent.Parts { get { return this.Parts.Cast<IReadOnlyContentPart>(); } }

        internal override string Name { get { return ContentName; } }
    }
}