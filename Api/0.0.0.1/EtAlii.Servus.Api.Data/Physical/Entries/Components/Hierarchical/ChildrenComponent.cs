namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ChildrenComponent : CompositeComponent 
    {
        [JsonConstructor]
        internal ChildrenComponent()
        { 
        }

        public ChildrenComponent(IEnumerable<Relation> children)
        {
            _children = children;
        }

        public IEnumerable<Relation> Children { get { return _children; } }
        private IEnumerable<Relation> _children = new Relation[] { };

        protected internal override string Name { get { return _name; } }
        private const string _name = "Children";

        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.ChildrenComponent.Add(Children, markAsStored);
        }
    }
}
