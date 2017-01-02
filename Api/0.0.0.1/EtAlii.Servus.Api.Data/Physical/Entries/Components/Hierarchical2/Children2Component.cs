namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Children2Component : CompositeComponent 
    {
        [JsonConstructor]
        internal Children2Component()
        { 
        }

        public Children2Component(IEnumerable<Relation> children2)
        {
            _children2 = children2;
        }

        public IEnumerable<Relation> Children2 { get { return _children2; } }
        private IEnumerable<Relation> _children2 = new Relation[] { };

        protected internal override string Name { get { return _name; } }
        private const string _name = "Children2";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.Children2Component.Add(Children2, markAsStored);
        }
    }
}
