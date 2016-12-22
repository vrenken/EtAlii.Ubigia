namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Servus.Client.Windows.Shared;
    using EtAlii.Servus.Windows;
    using EtAlii.xTechnology.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        public RootsViewModel RootsViewModel { get { return _rootsViewModel; } }
        private readonly RootsViewModel _rootsViewModel;

        public JournalViewModel JournalViewModel { get { return _journalViewModel; } }
        private readonly JournalViewModel _journalViewModel;

        public ObservableCollection<IDocumentViewModel> Documents { get { return _documents; } }
        private readonly ObservableCollection<IDocumentViewModel> _documents = new ObservableCollection<IDocumentViewModel>();

        public ICommand CloseCommand { get { return _closeCommand; } set { SetProperty(ref _closeCommand, value); } }
        private ICommand _closeCommand;

        public NewDocumentCommand[] NewBlankDocumentCommands { get { return _newBlankDocumentCommands; } set { SetProperty(ref _newBlankDocumentCommands, value); } }
        private NewDocumentCommand[] _newBlankDocumentCommands;

        public NewDocumentCommand[] NewDocumentFromTemplateCommands { get { return _newDocumentFromTemplateCommands; } set { SetProperty(ref _newDocumentFromTemplateCommands, value); } }
        private NewDocumentCommand[] _newDocumentFromTemplateCommands;

        public ICommand[] OpenDocumentFromFileCommands { get { return _openDocumentFromFileCommands; } set { SetProperty(ref _openDocumentFromFileCommands, value); } }
        private ICommand[] _openDocumentFromFileCommands;

        public ICommand[] OpenDocumentFromSpaceCommands { get { return _openDocumentFromSpaceCommands; } set { SetProperty(ref _openDocumentFromSpaceCommands, value); } }
        private ICommand[] _openDocumentFromSpaceCommands;

        public MainWindowViewModel(
            RootsViewModel rootsViewModel,
            JournalViewModel journalViewModel)
        {
            _rootsViewModel = rootsViewModel;
            _journalViewModel = journalViewModel;

            this.PropertyChanged += OnPropertyChanged;

            _closeCommand = new RelayCommand(ExecuteClose, CanClose);
        }

        private bool CanClose(object obj)
        {
            return true;
        }

        private void ExecuteClose(object obj)
        {
            Application.Current.Shutdown();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "NewBlankDocumentCommands":
                    break;
            }
        }
    }
}
