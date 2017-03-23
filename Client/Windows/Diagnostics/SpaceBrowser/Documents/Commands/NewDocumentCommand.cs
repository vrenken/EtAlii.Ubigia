namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewDocumentCommandBase : INewDocumentCommand
    {
        private readonly IFabricContext _fabricContext;
        private readonly IDataConnection _connection;
        private readonly IDataContext _dataContext;
        private readonly ILogicalContext _logicalContext;
        private readonly ILogger _logger;
        private readonly ILogFactory _logFactory;
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly IJournalViewModel _journal;
        private readonly IGraphContextFactory _graphContextFactory;

        private IMainWindowViewModel _mainWindowViewModel;

        public string Icon { get; set; } = "";

        public string Header { get; set; } = "";

        public string InfoLine { get; set; } = "";

        public string InfoTip1 { get; set; } = "";

        public string InfoTip2 { get; set; } = "";

        public string TitleFormat { get; set; } = "No name {0}";

        public IDocumentFactory DocumentFactory { get; protected set; }

        public NewDocumentCommandBase(
            IDataContext dataContext,
            ILogicalContext logicalContext,
            IFabricContext fabricContext,
            IDataConnection connection,
            ILogger logger,
            ILogFactory logFactory,
            IDiagnosticsConfiguration diagnostics,
            IJournalViewModel journal, 
            IGraphContextFactory graphContextFactory)
        {
            _dataContext = dataContext;
            _logicalContext = logicalContext;
            _fabricContext = fabricContext;
            _connection = connection;
            _logger = logger;
            _logFactory = logFactory;
            _diagnostics = diagnostics;
            _journal = journal;
            _graphContextFactory = graphContextFactory;
        }

        public void Initialize(IMainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            var title = DetermineTitle();

            var documentViewModel = DocumentFactory.Create(_dataContext, _logicalContext, _fabricContext, _connection, _diagnostics, _logger, _logFactory, _journal, _graphContextFactory);
            documentViewModel.Title = title;
            _mainWindowViewModel.Documents.Add(documentViewModel);
        }

        private string DetermineTitle()
        {
            var i = 0;
            var title = String.Empty;
            do
            {
                title = String.Format(TitleFormat, ++i);
            }
            while (_mainWindowViewModel.Documents.Any(g => g.Title == title));
            return title;
        }
    }
}
