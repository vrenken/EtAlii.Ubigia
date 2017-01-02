namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Workflow;

    public class ScriptViewModel : TextualViewModelBase, IScriptViewModel
    {
        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public ScriptButtonsViewModel Buttons { get { return _buttons; } }
        private readonly ScriptButtonsViewModel _buttons;

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

        public ObservableCollection<string> ExecutionStatus { get { return _executionStatus; } }
        private readonly ObservableCollection<string> _executionStatus = new ObservableCollection<string>();

        public event Action CodeChanged = delegate { };
        private IDisposable _scriptChangedSubscription;

        public ScriptViewModel(
            IUnitOfWorkProcessor unitOfWorkProcessor,
            IQueryProcessor queryProcessor,
            ScriptButtonsViewModel buttons)
        {
            _queryProcessor = queryProcessor;
            _unitOfWorkProcessor = unitOfWorkProcessor;
            _buttons = buttons;

            PropertyChanged += OnPropertyChanged;

            _scope = new ScriptScope();

            _scriptChangedSubscription = Observable.FromEvent((handler) => CodeChanged += handler, (handler) => CodeChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(e => _unitOfWorkProcessor.Process(new ParseScriptUnitOfwork(this)));

            Code = _queryProcessor.Process<string>(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.Script.Templates.SimpleScript.cs")).Single();
        }

        protected override void Execute(object obj)
        {
            _unitOfWorkProcessor.Process(new ProcessScriptUnitOfwork(this));
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
