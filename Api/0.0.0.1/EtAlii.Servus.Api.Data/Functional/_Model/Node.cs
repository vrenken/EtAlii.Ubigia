namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;

    public partial class Node : IInternalNode, INode, IEquatable<Node>
    {
        // TODO: There should be no properties on the Node base class.

        Identifier INode.Id { get { return _entry.Id; } }

        public string Type { get { return _entry.Type; } }

        //public Identifier Schema { get { return _schema; } private set { SetProperty(ref _schema, value); } }
        //private Identifier _schema;

        bool INode.IsModified { get { return _isModified; } }
        private bool _isModified;

        //public LinkCollection Links { get { return _links; } private set { SetProperty(ref _links, value); } }
        //private LinkCollection _links;

        IReadOnlyEntry IInternalNode.Entry { get { return _entry; } }// set { _entry = value; } }
        private IReadOnlyEntry _entry;

        public Node(IReadOnlyEntry entry)
        {
            _entry = entry;
        }

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