namespace EtAlii.Ubigia
{
    public abstract class RelationComponent : NonCompositeComponent
    {
        internal RelationComponent()
        { 
        }

        public Relation Relation { get; internal set; }
    }
}
