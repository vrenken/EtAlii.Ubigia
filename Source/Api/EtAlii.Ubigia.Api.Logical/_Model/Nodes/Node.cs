// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public partial class Node : IInternalNode
    {
        // Improve the way how Node and DynamicNode are used.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/84

        Identifier INode.Id => _entry.Id;

        public string Type => _entry.Type;

        bool INode.IsModified => _isModified;
        private bool _isModified;

        IReadOnlyEntry IInternalNode.Entry => _entry;
        private IReadOnlyEntry _entry;

        public Node(IReadOnlyEntry entry)
        {
            _entry = entry;
        }

        void IInternalNode.Update(PropertyDictionary properties, IReadOnlyEntry entry)
        {
            _entry = entry ?? throw new ArgumentNullException(nameof(entry));
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
            _isModified = false;
        }

        public override string ToString()
        {
            return _entry.Type;
        }
    }
}
