namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public partial class Node : IInternalNode
    {
        // TODO: There should be no properties on the Node base class.

        Identifier INode.Id => _entry.Id;

        public string Type => _entry.Type;

        //public Identifier Schema [ get [ return _schema; ] private set [ SetProperty(ref _schema, value) ] ]
        //private Identifier _schema

        bool INode.IsModified => _isModified;
        private bool _isModified;

        //public LinkCollection Links [ get [ return _links; ] private set [ SetProperty(ref _links, value) ] ]
        //private LinkCollection _links

        IReadOnlyEntry IInternalNode.Entry => _entry;
        // set [ _entry = value; ] ]
        private IReadOnlyEntry _entry;

        public Node(IReadOnlyEntry entry)
        {
            _entry = entry;
        }

        //void IInternalNode.ClearIsModified()
        //[
        //    _isModified = false
        //]
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