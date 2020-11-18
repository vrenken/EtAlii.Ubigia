namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public sealed class GraphScriptLanguageViewModel : TextualViewModelBase, IGraphScriptLanguageViewModel, IDisposable
    {

        public IScriptButtonsViewModel Buttons { get; }

        private readonly IGraphContext _graphContext;

        public string Source { get => _source; set => SetProperty(ref _source, value); }
        private string _source;

        public IScriptScope Scope { get => _scope; set => SetProperty(ref _scope, value); }
        private IScriptScope _scope;

        public Script Script { get => _script; set => SetProperty(ref _script, value); }
        private Script _script;

        public ObservableCollection<Result> ScriptVariables { get; } = new ObservableCollection<Result>();
        public ObservableCollection<Result> ScriptResults { get; } = new ObservableCollection<Result>();
        public ObservableCollection<string> ExecutionStatus { get; } = new ObservableCollection<string>();

        public event Action SourceChanged = delegate { };
        private readonly IDisposable _scriptChangedSubscription;

        private readonly IProcessGraphScriptLanguageUnitOfworkHandler _processScriptUnitOfworkHandler;


        public GraphScriptLanguageViewModel(
            IGraphContext graphContext,
            IScriptButtonsViewModel buttons, 
            ITextTemplateQueryHandler textTemplateQueryHandler, 
            IParseGraphScriptLanguageUnitOfworkHandler parseScriptUnitOfworkHandler, 
            IProcessGraphScriptLanguageUnitOfworkHandler processScriptUnitOfworkHandler)
        {
            _graphContext = graphContext;
            Buttons = buttons;
            _processScriptUnitOfworkHandler = processScriptUnitOfworkHandler;

            PropertyChanged += OnPropertyChanged;

            _scope = new ScriptScope();

            _scriptChangedSubscription = Observable.FromEvent((handler) => SourceChanged += handler, (handler) => SourceChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(_ => _graphContext.UnitOfWorkProcessor.Process(new ParseGraphScriptLanguageUnitOfwork(this), parseScriptUnitOfworkHandler));

            Source = _graphContext.QueryProcessor.Process(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.GraphScriptLanguage.Templates.SimpleScript.gsl"), textTemplateQueryHandler).Single();
        }

        protected override void Execute(object obj)
        {
            _graphContext.UnitOfWorkProcessor.Process(new ProcessGraphScriptLanguageUnitOfwork(this), _processScriptUnitOfworkHandler);
        }

        protected override void Pause(object obj)
        {
            if (CanStop)
            {
                //var results = _codeCompilerService.Compile(Code)
                //Errors = _codeCompilerResultsParser.Parse(results)
            }
        }

        protected override void Stop(object obj)
        {
            if (CanStop)
            {
                //var results = _codeCompilerService.Compile(Code)
                //Errors = _codeCompilerResultsParser.Parse(results)
            }
        }

        protected override bool CanClear(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Source);
        }

        protected override void Clear(object parameter)
        {
            Source = string.Empty;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Source):
                    SourceChanged();
                    break;
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scriptChangedSubscription?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~GraphScriptLanguageViewModel()
        {
            Dispose(false);
        }
    }
}
