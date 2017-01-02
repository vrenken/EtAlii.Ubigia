namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;

    public class DowndateComponent : NonCompositeComponent
    {
        [JsonConstructor]
        internal DowndateComponent()
        { 
        }

        public DowndateComponent(Relation downdate)
        {
            _downdate = downdate;
        }

        public Relation Downdate { get { return _downdate; } }
        private Relation _downdate;

        protected internal override string Name { get { return _name; } }
        private const string _name = "Downdate";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.DowndateComponent = this;
        }
    }
}
