namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface IMainWindowViewModel : INotifyPropertyChanged
    {
        IRootsViewModel RootsViewModel { get; }
        IJournalViewModel JournalViewModel { get; }
        ObservableCollection<IDocumentViewModel> Documents { get; }
        ICommand CloseCommand { get; set; }
        INewDocumentCommand[] NewBlankDocumentCommands { get; }
        INewDocumentCommand[] NewDocumentFromTemplateCommands { get; set; }
        ICommand[] OpenDocumentFromFileCommands { get; set; }
        ICommand[] OpenDocumentFromSpaceCommands { get; set; }
    }
}