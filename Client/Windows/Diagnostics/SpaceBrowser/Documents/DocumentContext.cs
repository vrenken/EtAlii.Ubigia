namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Querying;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    class DocumentContext : IDocumentContext
    {
        public DocumentContext(
            IGraphSLScriptContext scriptContext, 
            IGraphQLQueryContext queryContext, 
            ILinqQueryContext linqContext, 
            ILogicalContext logicalContext, 
            IFabricContext fabricContext, 
            IDataConnection connection, 
            IDiagnosticsConfiguration diagnostics, 
            ILogger logger, 
            ILogFactory logFactory, 
            IJournalViewModel journal, 
            IGraphContextFactory graphContextFactory)
        {
            ScriptContext = scriptContext;
            QueryContext = queryContext;
            LinqContext = linqContext;
            LogicalContext = logicalContext;
            FabricContext = fabricContext;
            Connection = connection;
            Diagnostics = diagnostics;
            Logger = logger;
            LogFactory = logFactory;
            Journal = journal;
            GraphContextFactory = graphContextFactory;
        }

        public IGraphSLScriptContext ScriptContext { get; }
        public IGraphQLQueryContext QueryContext { get; }
        public ILinqQueryContext LinqContext { get; }
        public ILogicalContext LogicalContext { get; }
        public IFabricContext FabricContext { get; }
        public IDataConnection Connection { get; }
        public IDiagnosticsConfiguration Diagnostics { get; }
        public ILogger Logger { get; }
        public ILogFactory LogFactory { get; }
        public IJournalViewModel Journal { get; }
        public IGraphContextFactory GraphContextFactory { get; }
    }
}