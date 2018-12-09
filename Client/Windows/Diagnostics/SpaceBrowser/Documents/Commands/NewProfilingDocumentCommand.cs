namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewProfilingDocumentCommand : NewDocumentCommandBase, INewProfilingDocumentCommand
    {
        public NewProfilingDocumentCommand(
            IGraphSLScriptContext graphSlScriptContext,
            IProfilingDocumentFactory factory,
            IDataContext dataContext, 
            ILogicalContext logicalContext, 
            IFabricContext fabricContext, 
            IDataConnection connection, 
            ILogger logger, 
            ILogFactory logFactory, 
            IDiagnosticsConfiguration diagnostics,
            IJournalViewModel journal,
            IGraphContextFactory graphContextFactory) 
            : base(graphSlScriptContext, dataContext, logicalContext, fabricContext, connection, logger, logFactory, diagnostics, journal, graphContextFactory)
        {
            DocumentFactory = factory;
            Header = "Profiling";
            Icon = @"pack://siteoforigin:,,,/Images/Arrow.png";
            TitleFormat = "Profiler view {0}";
            InfoLine = "Create a profiling document";
            InfoTip1 = "Shows profiling details of all API access to a space";
            InfoTip2 = "Useful for advanced query optimization";
        }
    }
}
