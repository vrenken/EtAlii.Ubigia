namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Functional;

    public class GraphQueryLanguageViewModel : TextualViewModelBase, IGraphQueryLanguageViewModel
    {
        private readonly IGraphContext _graphContext;

        public string Source { get => _source; set => SetProperty(ref _source, value); }
        private string _source;

//        public IQueryScope Scope { get { return _scope; } set { SetProperty(ref _scope, value); } }
//        private IQueryScope _scope;

        public Query Query { get => _query; set => SetProperty(ref _query, value); }
        private Query _query;

        public ObservableCollection<Result> QueryVariables { get; } = new ObservableCollection<Result>();
        
        public string QueryResult { get => _queryResult; set => SetProperty(ref _queryResult, value); }
        private string _queryResult = String.Empty;
        
        public ObservableCollection<string> ExecutionStatus { get; } = new ObservableCollection<string>();

        public event Action SourceChanged = delegate { };
        private readonly IDisposable _queryChangedSubscription;

        private readonly IProcessGraphQueryLanguageUnitOfworkHandler _processQueryUnitOfworkHandler;


        public GraphQueryLanguageViewModel(
            IGraphContext graphContext,
            ITextTemplateQueryHandler textTemplateQueryHandler, 
            IParseGraphQueryLanguageUnitOfworkHandler parseQueryUnitOfworkHandler, 
            IProcessGraphQueryLanguageUnitOfworkHandler processQueryUnitOfworkHandler)
        {
            _graphContext = graphContext;
            _processQueryUnitOfworkHandler = processQueryUnitOfworkHandler;

            PropertyChanged += OnPropertyChanged;

//            _scope = new QueryScope();

            _queryChangedSubscription = Observable.FromEvent((handler) => SourceChanged += handler, (handler) => SourceChanged -= handler)
                                .Throttle(TimeSpan.FromSeconds(1))
                                .ObserveOnDispatcher()
                                .Subscribe(e => _graphContext.UnitOfWorkProcessor.Process(new ParseGraphQueryLanguageUnitOfwork(this), parseQueryUnitOfworkHandler));

            Source = _graphContext.QueryProcessor.Process(new TextTemplateQuery("EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser.Textual.GraphQueryLanguage.Templates.SimpleQuery.gql"), textTemplateQueryHandler).Single();
        }

        protected override void Execute(object obj)
        {
            _graphContext.UnitOfWorkProcessor.Process(new ProcessGraphQueryLanguageUnitOfwork(this), _processQueryUnitOfworkHandler);
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
