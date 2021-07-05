// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    public partial class Node : IInternalNode
    {
        // Improve the way how Node and DynamicNode are used.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/84

        /// <inheritdoc />
        Identifier INode.Id => _entry.Id;

        /// <inheritdoc />
        public string Type => _entry.Type;

        /// <inheritdoc />
        IReadOnlyEntry IInternalNode.Entry => _entry;
        private readonly IReadOnlyEntry _entry;

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
