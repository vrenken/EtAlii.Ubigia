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

        public string Icon { get { return _icon; } set { _icon = value; } }
        private string _icon = "";

        public string Header { get { return _header; } set { _header = value; } }
        private string _header = "";

        public string InfoLine { get { return _infoLine; } set { _infoLine = value; } }
        private string _infoLine = "";

        public string InfoTip1 { get { return _infoTip1; } set { _infoTip1 = value; } }
        private string _infoTip1 = "";

        public string InfoTip2 { get { return _infoTip2; } set { _infoTip2 = value; } }
        private string _infoTip2 = "";

        public string TitleFormat { get { return _titleFormat; } set { _titleFormat = value; } }
        private string _titleFormat = "No name {0}";

        public IDocumentFactory DocumentFactory { get { return _documentFactory; } protected set { _documentFactory = value; } }
        private IDocumentFactory _documentFactory;

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
                title = String.Format(_titleFormat, ++i);
            }
            while (_mainWindowViewModel.Documents.Any(g => g.Title == title));
            return title;
        }
    }
}
