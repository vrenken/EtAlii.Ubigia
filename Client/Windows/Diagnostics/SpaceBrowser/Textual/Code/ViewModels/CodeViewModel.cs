namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.xTechnology.Workflow;

    public class CodeViewModel : TextualViewModelBase, ICodeViewModel
    {

        public ICodeButtonsViewModel Buttons => _buttons;
        private readonly IGraphContext _graphContext;
        private readonly ICodeButtonsViewModel _buttons;

        public string Code { get { return _code; } set { SetProperty(ref _code, value); } }
        private string _code;

        public event Action CodeChanged = delegate { };
        private IDisposable _codeChangedSubscription;

        private readonly ITextTemplateQueryHandler _textTemplateQueryHandler;
        private readonly ICompileCodeUnitOfworkHandler _compileCodeUnitOfworkHandler;
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
            _buttons = buttons;
            _textTemplateQueryHandler = textTemplateQueryHandler;
            _compileCodeUnitOfworkHandler = compileCodeUnitOfworkHandler;
            _executeCodeUnitOfworkHandler = executeCodeUnitOfworkHandler;

            PropertyChanged += OnPropertyChanged;

            _codeChangedSubscription = Observable.FromEvent((handler) => CodeChanged += handler, (handler) => CodeChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(e => _graphContext.UnitOfWorkProcessor.Process(new CompileCodeUnitOfwork(this), _compileCodeUnitOfworkHandler));

            Code = _graphContext.QueryProcessor.Process<string>(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.Code.Templates.SimpleCode.cs"), _textTemplateQueryHandler).Single();
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
