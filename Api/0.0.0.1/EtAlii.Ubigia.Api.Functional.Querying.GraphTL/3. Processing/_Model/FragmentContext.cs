namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;

    internal sealed class FragmentContext
    {
        public ObservableCollection<Structure> Children { get; }
        public ObservableCollection<Structure> Structures { get; }

        public ObservableCollection<Value> Values { get; }

        public FragmentContext(
            ObservableCollection<Structure> structures,
            ObservableCollection<Structure> children, ObservableCollection<Value> values)
        {
            Structures = structures;
            Children = children;
            Values  = values;
        }
    }
}
