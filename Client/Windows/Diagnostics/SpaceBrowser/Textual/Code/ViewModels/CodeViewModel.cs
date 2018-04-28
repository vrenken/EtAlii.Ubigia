namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;

    public class CodeViewModel : TextualViewModelBase, ICodeViewModel
    {

        public ICodeButtonsViewModel Buttons { get; }

        private readonly IGraphContext _graphContext;

        public string Code { get { return _code; } set { SetProperty(ref _code, value); } }
        private string _code;

        public event Action CodeChanged = delegate { };
        private IDisposable _codeChangedSubscription;

        private readonly IExecuteCodeUnitOfworkHandler _executeCodeUnitOfworkHandler;

        public CodeViewModel(
            IGraphContext graphContext,
            ICodeButtonsViewModel buttons,
            ICodeCompiler codeCompiler,
            ICodeCompilerResultsParser codeCompilerResultsParser, 
            ITextTemplateQueryHandler textTemplateQueryHandler, 
            ICompileCodeUnitOfworkHandler compileCodeUnitOfworkHandler, 
            IExecuteCodeUnitOfworkHandler executeCodeUnitOfworkHandler)
        {
            _graphContext = graphContext;
            Buttons = buttons;
            _executeCodeUnitOfworkHandler = executeCodeUnitOfworkHandler;

            PropertyChanged += OnPropertyChanged;

            _codeChangedSubscription = Observable.FromEvent((handler) => CodeChanged += handler, (handler) => CodeChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(e => _graphContext.UnitOfWorkProcessor.Process(new CompileCodeUnitOfwork(this), compileCodeUnitOfworkHandler));

            Code = _graphContext.QueryProcessor.Process(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.Code.Templates.SimpleCode.cs"), textTemplateQueryHandler).Single();
        }

        protected override void Execute(object obj)
        {
            _graphContext.UnitOfWorkProcessor.Process(new ExecuteCodeUnitOfwork(this), _executeCodeUnitOfworkHandler);
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
