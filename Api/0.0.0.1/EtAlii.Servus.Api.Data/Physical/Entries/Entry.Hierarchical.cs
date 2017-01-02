namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Entry
    {
        public Relation Parent { get { return _parent.Parent; } }
        private ParentComponent _parent;

        public IEnumerable<Relation> Children { get { return _children.SelectMany(component => component.Children); } }
        private readonly ChildrenComponentCollection _children;


        ChildrenComponentCollection IEditableEntry.Children
        {
            get
            {
                return _children;
            }
        }

        ChildrenComponentCollection IComponentEditableEntry.ChildrenComponent
        {
            get
            {
                return _children;
            }
        }

        Relation IEditableEntry.Parent
        {
            get
            {
                return _parent.Parent;
            }
            set
            {
                if (_parent.Parent != Relation.None)
                {
                    throw new InvalidOperationException("Unable to set Entry.Parent. This relation has already been assigned");
                }
                _parent = new ParentComponent(value);
            }
        }

        ParentComponent IComponentEditableEntry.ParentComponent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
    }
}
