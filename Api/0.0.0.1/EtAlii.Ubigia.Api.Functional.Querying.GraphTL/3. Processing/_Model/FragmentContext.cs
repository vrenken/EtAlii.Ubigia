using System;

namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;

    internal sealed class FragmentContext
    {
        public ObservableCollection<Structure> Children { get; }
        public ObservableCollection<Structure> ParentChildren { get; }

        public ObservableCollection<Value> Values { get; }

        public FragmentContext(
            ObservableCollection<Structure> parentChildren,
            ObservableCollection<Structure> children, ObservableCollection<Value> values)
        {
            ParentChildren = parentChildren;
            Children = children;
            Values  = values;
        }
    }
}
