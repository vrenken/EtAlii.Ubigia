namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using Northwoods.GoXam.Model;

    // This class holds sufficient information for all nodes in this sample.
    public class EntryNode : GraphLinksModelNodeData<Identifier>
    {
        public IReadOnlyEntry Entry
        {
            get { return _entry; }
            set { if (_entry != value) { var old = _entry; _entry = value; RaisePropertyChanged("Entry", old, value); } }
        }
        private IReadOnlyEntry _entry = null;

        public EntryNode(IReadOnlyEntry entry)
        {
            Entry = entry;
            Key = entry.Id;
        }
    }
}
