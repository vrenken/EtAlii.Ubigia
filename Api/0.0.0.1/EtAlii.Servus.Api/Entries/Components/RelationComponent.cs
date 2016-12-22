namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public abstract class RelationComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal RelationComponent()
        { 
        }

        public Relation Relation { get { return _relation; } internal set { _relation = value; } }
        private Relation _relation;


    }
}
