namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Functional;

    public interface IScriptViewModel : IDocumentViewModel
    {
        ObservableCollection<string> ExecutionStatus { get; }
        IScriptScope Scope { get; }
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

    }
}