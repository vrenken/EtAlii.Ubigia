namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Dynamic;

    public partial class DynamicNode : DynamicObject, IInternalNode, INode, IEquatable<DynamicNode>
    {
        private const string NotSupportedErrorMessage = "This action is not supported on DynamicNode instances";

        // TODO: There should be no properties on the Node base class.

        Identifier INode.Id => _entry.Id;
        string INode.Type => _entry.Type;

        private IReadOnlyEntry _entry;

        //public Identifier Schema { get { return _schema; } private set { SetProperty(ref _schema, value); } }
        //private Identifier _schema;

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
// set { _entry = value;} }

        //void IInternalNode.ClearIsModified()
        //{
        //    _isModified = false;
        //}

        void IInternalNode.Update(PropertyDictionary properties, IReadOnlyEntry entry)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            _entry = entry;
            _properties = properties;
            _isModified = false;
        }

        public override string ToString()
        {
            return _entry.Type;
        }
    }
}