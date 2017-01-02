using Newtonsoft.Json;
namespace EtAlii.Servus.Api
{
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
