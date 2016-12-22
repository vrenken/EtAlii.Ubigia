namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Mvvm;

    public class GraphConfiguration : BindableBase
    {
        public bool AddNewEntries { get { return _addNewEntries; } set { SetProperty(ref _addNewEntries, value); } }
        private bool _addNewEntries;

        public bool ExpandNewEntries { get { return _expandNewEntries; } set { SetProperty(ref _expandNewEntries, value); } }
        private bool _expandNewEntries;

        public bool ExpandSelectedEntries { get { return _expandSelectedEntries; } set { SetProperty(ref _expandSelectedEntries, value); } }
        private bool _expandSelectedEntries;

        public bool UpdateSelectedEntries { get { return _updateSelectedEntries; } set { SetProperty(ref _updateSelectedEntries, value); } }
        private bool _updateSelectedEntries;

        public bool ShowHierarchical { get { return _showHierarchical; } set { SetProperty(ref _showHierarchical, value); } }
        private bool _showHierarchical = true;

        public bool ShowSequential { get { return _showSequential; } set { SetProperty(ref _showSequential, value); } }
        private bool _showSequential = true;

        public bool ShowTemporal { get { return _showTemporal; } set { SetProperty(ref _showTemporal, value); } }
        private bool _showTemporal = true;

    }
}
