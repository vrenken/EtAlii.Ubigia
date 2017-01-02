namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Parent2 { get { return _parent2.Relation; } }
        private Parent2Component _parent2;

        public IEnumerable<Relation> Children2 { get { return _children2.SelectMany(component => component.Relations); } }
        private readonly Children2ComponentCollection _children2;


        Children2ComponentCollection IEditableEntry.Children2
        {
            get { return _children2; }
        }

        Children2ComponentCollection IComponentEditableEntry.Children2Component
        {
            get { return _children2; }
        }

        Relation IEditableEntry.Parent2
        {
            get { return _parent2.Relation; }
            set
            {
                if (_parent2.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Parent2. This relation has already been assigned");
                }
                _parent2 = new Parent2Component { Relation = value };
            }
        }

        Parent2Component IComponentEditableEntry.Parent2Component
        {
            get { return _parent2; }
            set { _parent2 = value; }
        }
    }
}
