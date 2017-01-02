namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Parent { get { return _parent.Relation; } }
        private ParentComponent _parent;

        public IEnumerable<Relation> Children { get { return _children.SelectMany(component => component.Relations); } }
        private readonly ChildrenComponentCollection _children;


        ChildrenComponentCollection IEditableEntry.Children
        {
            get { return _children; }
        }

        ChildrenComponentCollection IComponentEditableEntry.ChildrenComponent
        {
            get { return _children; }
        }

        Relation IEditableEntry.Parent
        {
            get { return _parent.Relation; }
            set
            {
                if (_parent.Relation != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Parent. This relation has already been assigned");
                }
                _parent = new ParentComponent { Relation = value };
            }
        }

        ParentComponent IComponentEditableEntry.ParentComponent
        {
            get { return _parent; }
            set { _parent = value; }
        }
    }
}
