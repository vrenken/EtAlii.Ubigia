namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public partial class DynamicNode : DynamicObject, IInternalNode, INode, IEquatable<DynamicNode>
    {
        private const string NotSupportedErrorMessage = "This action is not supported on DynamicNode instances";

        // TODO: There should be no properties on the Node base class.

        Identifier INode.Id { get { return _entry.Id; } }

        public string Type { get { return _entry.Type; } }

        //public Identifier Schema { get { return _schema; } private set { SetProperty(ref _schema, value); } }
        //private Identifier _schema;

        bool INode.IsModified { get { return _isModified; } }
        private bool _isModified;

        private IReadOnlyEntry _entry;

        internal DynamicNode(IReadOnlyEntry entry, IDictionary<string, object> properties)
        {
            _entry = entry;
            _properties = properties;
        }

        public DynamicNode(IReadOnlyEntry entry)
        {
            _entry = entry;
        }

        IReadOnlyEntry IInternalNode.Entry { get { return _entry; } }// set { _entry = value;} }

        //void IInternalNode.ClearIsModified()
        //{
        //    _isModified = false;
        //}

        void IInternalNode.Update(IDictionary<string, object> properties, IReadOnlyEntry entry)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }

            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            _entry = entry;
            _properties = properties;
            _isModified = false;
        }
    }
}