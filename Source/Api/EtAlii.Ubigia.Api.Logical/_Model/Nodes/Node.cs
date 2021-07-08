// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Dynamic;

    public partial class Node : DynamicObject
    {
        // Improve the way how Node and DynamicNode are used.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/84
        // Either we do:
        // Everything through dynamic => Then there should be no own properties on the Node.
        // We get rid of the DynamicObject => Then the properties should be accessed through the corresponding methods.

         /// <summary>
         /// The graph entry that the node is wrapping.
         /// </summary>
        public Identifier Id => _entry.Id;

        public string Type => _entry.Type;

        public IReadOnlyEntry Entry => _entry;
        private readonly IReadOnlyEntry _entry;

        public Node(Entry entry)
        {
            _entry = entry;
            _properties = new();
        }
        public Node(IEditableEntry entry)
        {
            _entry = (IReadOnlyEntry)entry;
            _properties = new();
        }


        public Node(IReadOnlyEntry entry)
        {
            _entry = entry;
            _properties = new();
        }

        public Node(IReadOnlyEntry entry, PropertyDictionary property)
        {
            _entry = entry;
            _properties = property;
        }

        public override string ToString()
        {
            return _entry.Type;
        }
    }
}
