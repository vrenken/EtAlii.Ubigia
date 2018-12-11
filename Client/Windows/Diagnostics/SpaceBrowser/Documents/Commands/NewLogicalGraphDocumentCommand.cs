namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewLogicalGraphDocumentCommand : NewDocumentCommandBase, INewLogicalGraphDocumentCommand
    {
        public NewLogicalGraphDocumentCommand(
            IGraphSLScriptContext scriptContext,
            IGraphQLQueryContext queryContext,
            ILogicalGraphDocumentFactory factory,
            ILogicalContext logicalContext, 
            IFabricContext fabricContext, 
            IDataConnection connection, 
            ILogger logger, 
            ILogFactory logFactory, 
            IDiagnosticsConfiguration diagnostics,
            IJournalViewModel journal,
            IGraphContextFactory graphContextFactory) 
            : base(scriptContext, queryContext, logicalContext, fabricContext, connection, logger, logFactory, diagnostics, journal, graphContextFactory)
        {
            DocumentFactory = factory;
            Header = "Logical graph";
            Icon = @"pack://siteoforigin:,,,/Images/Nodes.png";
            TitleFormat = "Logical graph view {0}";
            InfoLine = "Create a document that shows a information stored in a space using a logical graph";
            InfoTip1 = "Useful for change analysis";
            InfoTip2 = "Shows temporal information";
        }
    }
}
