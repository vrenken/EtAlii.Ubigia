// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Dynamic;

    public sealed partial class DynamicNode : DynamicObject, IInternalNode
    {
        private const string NotSupportedErrorMessage = "This action is not supported on DynamicNode instances";

        // Improve the way how Node and DynamicNode are used.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/84

        /// <inheritdoc />
        Identifier INode.Id => _entry.Id;

        /// <inheritdoc />
        string INode.Type => _entry.Type;

        /// <inheritdoc />
        IReadOnlyEntry IInternalNode.Entry => _entry;
        private readonly IReadOnlyEntry _entry;

        public DynamicNode(IReadOnlyEntry entry)
        {
            _entry = entry;
            _properties = new ();
        }

        internal DynamicNode(IReadOnlyEntry entry, PropertyDictionary properties)
        {
            _entry = entry;
            _properties = properties;
        }

        public override string ToString()
        {
            return _entry.Type;
        }
    }
}
