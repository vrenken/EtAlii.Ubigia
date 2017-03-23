namespace EtAlii.Ubigia.Api
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class RelationsComponent : CompositeComponent
    {
        [JsonConstructor]
        internal RelationsComponent()
        { 
        }

        public IEnumerable<Relation> Relations { get; internal set; } = new Relation[] { };
    }
}
