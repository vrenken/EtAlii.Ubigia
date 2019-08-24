namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;
    using Northwoods.GoXam.Model;

    // This class holds sufficient information for all nodes in this sample.
    public class EntryNode : GraphLinksModelNodeData<Identifier>
    {
        public IReadOnlyEntry Entry
        {
            get => _entry;
            set { if (Equals(_entry, value)) return; var old = _entry; _entry = value; RaisePropertyChanged("Entry", old, value); }
        }
        private IReadOnlyEntry _entry;

        public EntryNode(IReadOnlyEntry entry)
        {
            Entry = entry;
            Key = entry.Id;
        }
    }
}
