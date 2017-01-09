namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Client.Windows.Shared;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.Mvvm;

    public class MainWindowViewModel : BindableBase, IMainWindowViewModel
    {
        public RootsViewModel RootsViewModel { get { return _rootsViewModel; } }
        private readonly RootsViewModel _rootsViewModel;

        public JournalViewModel JournalViewModel { get { return _journalViewModel; } }
        private readonly JournalViewModel _journalViewModel;

        public ObservableCollection<IDocumentViewModel> Documents { get { return _documents; } }
        private readonly ObservableCollection<IDocumentViewModel> _documents = new ObservableCollection<IDocumentViewModel>();

        public ICommand CloseCommand { get { return _closeCommand; } set { SetProperty(ref _closeCommand, value); } }
        private ICommand _closeCommand;

        public INewDocumentCommand[] NewBlankDocumentCommands { get { return _newBlankDocumentCommands; } }
        private readonly INewDocumentCommand[] _newBlankDocumentCommands;

        public INewDocumentCommand[] NewDocumentFromTemplateCommands { get { return _newDocumentFromTemplateCommands; } set { SetProperty(ref _newDocumentFromTemplateCommands, value); } }
        private INewDocumentCommand[] _newDocumentFromTemplateCommands;

        public ICommand[] OpenDocumentFromFileCommands { get { return _openDocumentFromFileCommands; } set { SetProperty(ref _openDocumentFromFileCommands, value); } }
        private ICommand[] _openDocumentFromFileCommands;

        public ICommand[] OpenDocumentFromSpaceCommands { get { return _openDocumentFromSpaceCommands; } set { SetProperty(ref _openDocumentFromSpaceCommands, value); } }
        private ICommand[] _openDocumentFromSpaceCommands;

        public MainWindowViewModel(
            RootsViewModel rootsViewModel,
            JournalViewModel journalViewModel,
            INewFunctionalGraphDocumentCommand newFunctionalGraphDocumentCommand,
            INewLogicalGraphDocumentCommand newLogicalGraphDocumentCommand,
            INewTreeDocumentCommand newTreeDocumentCommand,
            INewSequentialDocumentCommand newSequentialDocumentCommand,
            INewTemporalDocumentCommand newTemporalDocumentCommand,
            INewScriptDocumentCommand newScriptDocumentCommand,
            INewCodeDocumentCommand newCodeDocumentCommand,
            INewProfilingDocumentCommand newProfilingDocumentCommand)
        {
            _rootsViewModel = rootsViewModel;
            _journalViewModel = journalViewModel;

            _newBlankDocumentCommands = new INewDocumentCommand[]
            {
                newFunctionalGraphDocumentCommand,
                newLogicalGraphDocumentCommand,
                newTreeDocumentCommand,
                newSequentialDocumentCommand,
                newTemporalDocumentCommand,
                newScriptDocumentCommand,
                newCodeDocumentCommand,
                newProfilingDocumentCommand
            };

            foreach (var command in _newBlankDocumentCommands)
            {
                command.Initialize(this);
            }

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
