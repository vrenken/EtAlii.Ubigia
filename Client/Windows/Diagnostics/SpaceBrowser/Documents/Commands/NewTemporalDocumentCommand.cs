namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewTemporalDocumentCommand : NewDocumentCommandBase, INewTemporalDocumentCommand
    {
        public NewTemporalDocumentCommand(
            ITemporalDocumentFactory factory,
            IDataContext dataContext, 
            ILogicalContext logicalContext, 
            IFabricContext fabricContext, 
            IDataConnection connection, 
            ILogger logger, 
            ILogFactory logFactory, 
            IDiagnosticsConfiguration diagnostics,
            IJournalViewModel journal,
            IGraphContextFactory graphContextFactory) 
            : base(dataContext, logicalContext, fabricContext, connection, logger, logFactory, diagnostics, journal, graphContextFactory)
        {
            DocumentFactory = factory;
            Header = "Temporal";
            Icon = @"pack://siteoforigin:,,,/Images/Clock-01.png";
            TitleFormat = "Temporal view {0}";
            InfoLine = "Create a document to show information stored in a space temporal";
            InfoTip1 = "Usefull for temporal analysis";
            InfoTip2 = null;
        }
    }
}
