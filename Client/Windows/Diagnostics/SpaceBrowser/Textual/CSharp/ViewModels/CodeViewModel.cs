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

        public string Source { get { return _source; } set { SetProperty(ref _source, value); } }
        private string _source;

        public event Action SourceChanged = delegate { };
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

            _codeChangedSubscription = Observable.FromEvent((handler) => SourceChanged += handler, (handler) => SourceChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(e => _graphContext.UnitOfWorkProcessor.Process(new CompileCodeUnitOfwork(this), compileCodeUnitOfworkHandler));

            Source = _graphContext.QueryProcessor.Process(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.CSharp.Templates.SimpleCode.txt"), textTemplateQueryHandler).Single();
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
    }
}
