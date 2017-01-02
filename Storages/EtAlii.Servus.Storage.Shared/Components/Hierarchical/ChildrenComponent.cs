namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ChildrenComponent : CompositeComponent 
    {
        [JsonConstructor]
        protected ChildrenComponent()
        { 
        }

        public ChildrenComponent(IEnumerable<Relation> children)
        {
            _children = children;
        }

        public IEnumerable<Relation> Children { get { return _children; } }
        private IEnumerable<Relation> _children = new Relation[] { };

        internal override string Name{ get { return _name; } }
        private const string _name = "Children";

        public override void Apply(IEditableEntry entry)
        {
            entry.Children.AddRange(Children);
        }
    }
}
