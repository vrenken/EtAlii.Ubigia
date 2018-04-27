namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Functional;

    public class ScriptViewModel : TextualViewModelBase, IScriptViewModel
    {

        public IScriptButtonsViewModel Buttons { get; }

        private readonly IGraphContext _graphContext;

        public string Code { get { return _code; } set { SetProperty(ref _code, value); } }
        private string _code;

        public IScriptScope Scope { get { return _scope; } set { SetProperty(ref _scope, value); } }
        private IScriptScope _scope;

        public Script Script { get { return _script; } set { SetProperty(ref _script, value); } }
        private Script _script;

        public ObservableCollection<Result> ScriptVariables { get { return _scriptVariables; } set { SetProperty(ref _scriptVariables, value); } }
        private ObservableCollection<Result> _scriptVariables;

        public ObservableCollection<Result> ScriptResults { get { return _scriptResults; } set { SetProperty(ref _scriptResults, value); } }
        private ObservableCollection<Result> _scriptResults;

        public ObservableCollection<string> ExecutionStatus { get; } = new ObservableCollection<string>();

        public event Action CodeChanged = delegate { };
        private IDisposable _scriptChangedSubscription;

        private readonly IProcessScriptUnitOfworkHandler _processScriptUnitOfworkHandler;


        public ScriptViewModel(
            IGraphContext graphContext,
            IScriptButtonsViewModel buttons, 
            ITextTemplateQueryHandler textTemplateQueryHandler, 
            IParseScriptUnitOfworkHandler parseScriptUnitOfworkHandler, 
            IProcessScriptUnitOfworkHandler processScriptUnitOfworkHandler)
        {
            _graphContext = graphContext;
            Buttons = buttons;
            _processScriptUnitOfworkHandler = processScriptUnitOfworkHandler;

            PropertyChanged += OnPropertyChanged;

            _scope = new ScriptScope();

            _scriptChangedSubscription = Observable.FromEvent((handler) => CodeChanged += handler, (handler) => CodeChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(e => _graphContext.UnitOfWorkProcessor.Process(new ParseScriptUnitOfwork(this), parseScriptUnitOfworkHandler));

            Code = _graphContext.QueryProcessor.Process(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.Script.Templates.SimpleScript.cs"), textTemplateQueryHandler).Single();
        }

        protected override void Execute(object obj)
        {
            _graphContext.UnitOfWorkProcessor.Process(new ProcessScriptUnitOfwork(this), _processScriptUnitOfworkHandler);
        }

        protected override void Pause(object obj)
        {
            if (CanStop)
            {
                //var results = _codeCompilerService.Compile(Code);
                //Errors = _codeCompilerResultsParser.Parse(results);
            }
        }

        protected override void Stop(object obj)
        {
            if (CanStop)
            {
                //var results = _codeCompilerService.Compile(Code);
                //Errors = _codeCompilerResultsParser.Parse(results);
            }
        }

        protected override bool CanClear(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Code);
        }

        protected override void Clear(object parameter)
        {
            Code = string.Empty;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Code":
                    CodeChanged();
                    break;
            }
        }
    }
}
