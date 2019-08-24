namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public interface IGraphScriptLanguageViewModel : IDocumentViewModel, IExecutionStatusProvider
    {
        IScriptScope Scope { get; }
        IScriptButtonsViewModel Buttons { get; }
        string Source { get; set; }
        Script Script { get; set; }

        ObservableCollection<Result> ScriptVariables { get; }

        ObservableCollection<Result> ScriptResults { get; }

        IEnumerable<TextualError> Errors { get; set; }
        bool CanExecute { get; set; }
        bool CanStop { get; set; }
        ICommand ClearCommand { get; }
        ICommand ExecuteCommand { get; }
        ICommand PauseCommand { get; }
        ICommand StopCommand { get; }
        event System.Action SourceChanged;

    }
}