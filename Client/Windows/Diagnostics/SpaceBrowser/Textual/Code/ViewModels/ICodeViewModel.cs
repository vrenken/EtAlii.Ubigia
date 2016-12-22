namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;

    public interface ICodeViewModel : IDocumentViewModel
    {
        CodeButtonsViewModel Buttons { get; }
        string Code { get; set; }
        IEnumerable<TextualError> Errors { get; set; }
        bool CanExecute { get; set; }
        bool CanStop { get; set; }
        ICommand ClearCommand { get; }
        ICommand ExecuteCommand { get; }
        ICommand PauseCommand { get; }
        ICommand StopCommand { get; }
        event System.Action CodeChanged;

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
    }
}