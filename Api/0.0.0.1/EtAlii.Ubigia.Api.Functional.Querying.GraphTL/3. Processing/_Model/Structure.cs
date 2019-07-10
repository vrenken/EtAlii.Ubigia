namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api.Logical;

    public sealed class Structure
    {
        //public Identifier Id { get; }
        public string Type { get; }
        public string Name { get; }

        public ReadOnlyObservableCollection<Structure> Children { get; }
        internal ObservableCollection<Structure> EditableChildren { get; }

        public ReadOnlyObservableCollection<Value> Values { get; }
        internal ObservableCollection<Value> EditableValues { get; }

        internal IInternalNode Node { get; } 
        internal Structure Parent { get; } 
        
        internal Structure(string type, string name, Structure parent)
        {
            Type = type;
            Name = name;
            
            EditableChildren = new ObservableCollection<Structure>();
            Children = new ReadOnlyObservableCollection<Structure>(EditableChildren);
            EditableValues = new ObservableCollection<Value>();
            Values  = new ReadOnlyObservableCollection<Value>(EditableValues);
            
            Parent = parent;
        }

        internal Structure(string type, string name, Structure parent, IInternalNode node)
            : this(type, name, parent)
        {
            Node = node;
        }
    }
}
