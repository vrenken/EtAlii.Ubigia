namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewSequentialDocumentCommand : NewDocumentCommandBase, INewSequentialDocumentCommand
    {
        public NewSequentialDocumentCommand(
            ISequentialDocumentFactory factory,
            IDataContext dataContext, 
            ILogicalContext logicalContext, 
            IFabricContext fabricContext, 
            IDataConnection connection, 
            ILogger logger, 
            ILogFactory logFactory, 
            IDiagnosticsConfiguration diagnostics,
            IJournalViewModel journal) 
            : base(dataContext, logicalContext, fabricContext, connection, logger, logFactory, diagnostics, journal)
        {
            DocumentFactory = factory;
            Header = "Sequential";
            Icon = @"\Images\Icons\View-Details.png";
            TitleFormat = "Sequential view {0}";
            InfoLine = "Create a document to show information stored in a space sequentially";
            InfoTip1 = "Usefull for order analysis";
            InfoTip2 = "Does not show temporal information";
        }
    }
}
