namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    public class EntryConfiguration : BindableBase
    {
        public bool AutoAdd { get { return _autoAdd; } set { SetProperty(ref _autoAdd, value); } }
        private bool _autoAdd;

        public bool AutoRelate { get { return _autoRelate; } set { SetProperty(ref _autoRelate, value); } }
        private bool _autoRelate;

        public bool DiscoverNeighbors { get { return _discoverNeighbors; } set { SetProperty(ref _discoverNeighbors, value); } }
        private bool _discoverNeighbors;

        public bool ApplyLayout { get { return _applyLayout; } set { SetProperty(ref _applyLayout, value); } }
        private bool _applyLayout;
    }
}
