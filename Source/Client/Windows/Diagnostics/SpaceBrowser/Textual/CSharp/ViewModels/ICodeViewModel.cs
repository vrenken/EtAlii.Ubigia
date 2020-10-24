namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface ICodeViewModel : IDocumentViewModel
    {
        ICodeButtonsViewModel Buttons { get; }
        string Source { get; set; }
        IEnumerable<TextualError> Errors { get; set; }
        bool CanExecute { get; set; }
        bool CanStop { get; set; }
        ICommand ClearCommand { get; }
        ICommand ExecuteCommand { get; }
        ICommand PauseCommand { get; }
        ICommand StopCommand { get; }
        event System.Action SourceChanged;

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
    }
}