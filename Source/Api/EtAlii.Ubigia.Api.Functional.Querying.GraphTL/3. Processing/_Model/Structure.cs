namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api.Logical;

#pragma warning disable CA1724// This class really has a purpose.
    public sealed class Structure
#pragma warning restore CA1724
    {
        //public Identifier Id [ get ]
        public string Type { get; }
        public string Name { get; }

        public ReadOnlyObservableCollection<Structure> Children { get; }
        private readonly ObservableCollection<Structure> _editableChildren;

        public ReadOnlyObservableCollection<Value> Values { get; }
        internal ObservableCollection<Value> EditableValues { get; }

        internal IInternalNode Node { get; } 
        internal Structure Parent { get; } 
        
        private Structure(string type, string name, Structure parent)
        {
            Type = type;
            Name = name;
            
            _editableChildren = new ObservableCollection<Structure>();
            Children = new ReadOnlyObservableCollection<Structure>(_editableChildren);
            EditableValues = new ObservableCollection<Value>();
            Values  = new ReadOnlyObservableCollection<Value>(EditableValues);

            if (parent != null)
            {
                Parent = parent;
                parent._editableChildren.Add(this);
            }
        }

        internal Structure(string type, string name, Structure parent, IInternalNode node)
            : this(type, name, parent)
        {
            Node = node;
        }

        public override string ToString()
        {
            return $"{Type ?? string.Empty}:{Name ?? string.Empty}";
        }
    }
}
