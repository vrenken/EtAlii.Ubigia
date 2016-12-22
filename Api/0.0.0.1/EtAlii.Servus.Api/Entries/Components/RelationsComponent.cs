namespace EtAlii.Servus.Api
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

        public IEnumerable<Relation> Relations { get { return _relations; } internal set { _relations = value; } }
        private IEnumerable<Relation> _relations = new Relation[] { };
    }
}
