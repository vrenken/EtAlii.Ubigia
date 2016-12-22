namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using EtAlii.Servus.Api.Functional;

    public interface IScriptViewModel : IDocumentViewModel
    {
        ScriptButtonsViewModel Buttons { get; }
        string Code { get; set; }
        Script Script { get; set; }

        ObservableCollection<Result> ScriptVariables { get; set; }

        ObservableCollection<Result> ScriptResults { get; set; }

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