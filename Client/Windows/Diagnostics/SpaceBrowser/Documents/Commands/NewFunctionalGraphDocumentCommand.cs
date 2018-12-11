namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewFunctionalGraphDocumentCommand : NewDocumentCommandBase, INewFunctionalGraphDocumentCommand
    {
        public NewFunctionalGraphDocumentCommand(
            IGraphSLScriptContext scriptContext,
            IGraphQLQueryContext queryContext,
            IFunctionalGraphDocumentFactory factory,
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
            Header = "Functional graph";
            Icon = @"pack://siteoforigin:,,,/Images/Nodes.png";
            TitleFormat = "Functional graph view {0}";
            InfoLine = "Create a document that shows a information stored in a space using a functional graph";
            InfoTip1 = "Useful for current state analysis";
            InfoTip2 = "Does not show temporal information";
        }
    }
}
