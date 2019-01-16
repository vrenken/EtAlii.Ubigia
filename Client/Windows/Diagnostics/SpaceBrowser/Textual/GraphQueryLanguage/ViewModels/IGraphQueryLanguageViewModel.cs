namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Functional;

    public interface IGraphQueryLanguageViewModel : IDocumentViewModel, IExecutionStatusProvider
    {
        //IScriptScope Scope { get; }
        string Source { get; set; }
        Query Query { get; set; }

        ObservableCollection<Result> QueryVariables { get; }

        string QueryResult { get; set; }

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