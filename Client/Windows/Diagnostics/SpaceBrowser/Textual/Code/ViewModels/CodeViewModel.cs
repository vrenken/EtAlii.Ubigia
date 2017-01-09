namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.xTechnology.Workflow;

    public class CodeViewModel : TextualViewModelBase, ICodeViewModel
    {
        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public ICodeButtonsViewModel Buttons { get { return _buttons; } }
        private readonly ICodeButtonsViewModel _buttons;

        public string Code { get { return _code; } set { SetProperty(ref _code, value); } }
        private string _code;

        public event Action CodeChanged = delegate { };
        private IDisposable _codeChangedSubscription;

        public CodeViewModel(
            IUnitOfWorkProcessor unitOfWorkProcessor,
            IQueryProcessor queryProcessor,
            ICodeButtonsViewModel buttons,
            ICodeCompiler codeCompiler,
            ICodeCompilerResultsParser codeCompilerResultsParser)
        {
            _queryProcessor = queryProcessor;
            _unitOfWorkProcessor = unitOfWorkProcessor;
            _buttons = buttons;

            PropertyChanged += OnPropertyChanged;

            _codeChangedSubscription = Observable.FromEvent((handler) => CodeChanged += handler, (handler) => CodeChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(e => _unitOfWorkProcessor.Process(new CompileCodeUnitOfwork(this)));

            Code = _queryProcessor.Process<string>(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.Code.Templates.SimpleCode.cs")).Single();
        }

        protected override void Execute(object obj)
        {
            _unitOfWorkProcessor.Process(new ExecuteCodeUnitOfwork(this));
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
