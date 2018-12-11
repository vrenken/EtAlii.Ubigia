namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewTreeDocumentCommand : NewDocumentCommandBase, INewTreeDocumentCommand
    {
        public NewTreeDocumentCommand(
            IGraphSLScriptContext scriptContext,
            IGraphQLQueryContext queryContext,
            ITreeDocumentFactory factory,
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
            Header = "Hierarchical";
            Icon = @"pack://siteoforigin:,,,/Images/Tree.png";
            TitleFormat = "Tree view {0}";
            InfoLine = "Create a document that shows information stored in a space hierarchically";
            InfoTip1 = "Useful for tree structure analysis";
            InfoTip2 = "Does not show temporal information";
        }
    }
}
