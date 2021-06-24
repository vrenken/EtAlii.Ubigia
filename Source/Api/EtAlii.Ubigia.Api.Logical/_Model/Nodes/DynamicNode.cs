// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Dynamic;

    public sealed partial class DynamicNode : DynamicObject, IInternalNode
    {
        private const string NotSupportedErrorMessage = "This action is not supported on DynamicNode instances";

        // TODO: There should be no properties on the Node base class.

        Identifier INode.Id => _entry.Id;
        string INode.Type => _entry.Type;

        private IReadOnlyEntry _entry;

        bool INode.IsModified => _isModified;
        private bool _isModified;

        internal DynamicNode(IReadOnlyEntry entry, PropertyDictionary properties)
        {
            _entry = entry;
            _properties = properties;
        }

        public DynamicNode(IReadOnlyEntry entry)
        {
            _entry = entry;
        }

        IReadOnlyEntry IInternalNode.Entry => _entry;

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
