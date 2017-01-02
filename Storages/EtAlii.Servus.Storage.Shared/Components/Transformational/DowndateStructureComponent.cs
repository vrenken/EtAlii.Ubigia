namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;

    public class DowndateComponent : NonCompositeComponent
    {
        [JsonConstructor]
        protected DowndateComponent()
        { 
        }

        public DowndateComponent(Relation downdate)
        {
            _downdate = downdate;
        }

        public Relation Downdate { get { return _downdate; } }
        private Relation _downdate;

        internal override string Name { get { return _name; } }
        private const string _name = "Downdate";

        public override void Apply(IEditableEntry entry)
        {
            entry.Downdate = Downdate;
        }
    }
}
