namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Windows.Mvvm;

    public class GraphConfiguration : BindableBase, IGraphConfiguration
    {
        public bool AddNewEntries { get => _addNewEntries; set => SetProperty(ref _addNewEntries, value); }
        private bool _addNewEntries;

        public bool ExpandNewEntries { get => _expandNewEntries; set => SetProperty(ref _expandNewEntries, value); }
        private bool _expandNewEntries;

        public bool ExpandSelectedEntries { get => _expandSelectedEntries; set => SetProperty(ref _expandSelectedEntries, value); }
        private bool _expandSelectedEntries;

        public bool UpdateSelectedEntries { get => _updateSelectedEntries; set => SetProperty(ref _updateSelectedEntries, value); }
        private bool _updateSelectedEntries;

        public bool ShowHierarchical { get => _showHierarchical; set => SetProperty(ref _showHierarchical, value); }
        private bool _showHierarchical = true;

        public bool ShowSequential { get => _showSequential; set => SetProperty(ref _showSequential, value); }
        private bool _showSequential = true;

        public bool ShowTemporal { get => _showTemporal; set => SetProperty(ref _showTemporal, value); }
        private bool _showTemporal = true;

    }
}
