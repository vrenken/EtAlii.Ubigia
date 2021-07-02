// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api.Logical;

#pragma warning disable CA1724 // This class really has a purpose.
    public sealed class Structure
#pragma warning restore CA1724
    {
        public string Type { get; }
        public string Name { get; }

        public ReadOnlyObservableCollection<Structure> Children { get; }
        private readonly ObservableCollection<Structure> _children;

        public ReadOnlyObservableCollection<Value> Values { get; }
        internal ObservableCollection<Value> EditableValues { get; }

        internal IInternalNode Node { get; }
        internal Structure Parent { get; }

        private Structure(string type, string name, Structure parent)
        {
            Type = type;
            Name = name;

            _children = new ObservableCollection<Structure>();
            Children = new ReadOnlyObservableCollection<Structure>(_children);
            EditableValues = new ObservableCollection<Value>();
            Values  = new ReadOnlyObservableCollection<Value>(EditableValues);

            Parent = parent;
            Parent?.AddChild(this);
        }

        internal Structure(string type, string name, Structure parent, IInternalNode node)
            : this(type, name, parent)
        {
            Node = node;
        }

        private void AddChild(Structure child)
        {
            _children.Add(child);
        }

        public override string ToString()
        {
            return $"{Type ?? string.Empty}:{Name ?? string.Empty}";
        }
    }
}
