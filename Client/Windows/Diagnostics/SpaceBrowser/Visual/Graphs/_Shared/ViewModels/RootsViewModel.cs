namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Windows.Mvvm;

    public class RootsViewModel : BindableBase, IRootsViewModel
    {
        public IEnumerable<Root> AvailableRoots { get { return _availableRoots; } set { SetProperty(ref _availableRoots, value); } }
        private IEnumerable<Root> _availableRoots;

        public Root SelectedRoot { get { return _selectedRoot; } set { SetProperty(ref _selectedRoot, value); } }
        private Root _selectedRoot;


        protected IFabricContext Fabric { get; }

        public ICommand BeginEntryDragCommand { get; }

        public RootsViewModel(IFabricContext fabric)
        {
            Fabric = fabric;
            BeginEntryDragCommand = new RelayCommand(BeginEntryDrag, CanBeginEntryDrag);
            ReloadAvailableRoots();
        }

        private void BeginEntryDrag(object obj)
        {
            var frameworkElement = obj as FrameworkElement;
            DragDrop.DoDragDrop(frameworkElement, SelectedRoot.Identifier, DragDropEffects.Move);
        }

        private bool CanBeginEntryDrag(object obj)
        {
            return _selectedRoot != null && obj is FrameworkElement;    
        }

        private void ReloadAvailableRoots()
        {
            var task = Task.Run(async () =>
            {
                AvailableRoots = await Fabric.Roots.GetAll();
            });
            task.Wait();
        }
    }
}
