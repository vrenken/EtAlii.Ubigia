namespace EtAlii.Ubigia.Api
{
    using Newtonsoft.Json;

    public abstract class RelationComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal RelationComponent()
        { 
        }

        public Relation Relation { get; internal set; }
    }
}
