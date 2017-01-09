namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
            IFunctionalGraphDocumentFactory factory,
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
            Header = "Functional graph";
            Icon = @"\Images\Icons\Nodes.png";
            TitleFormat = "Graph view {0}";
            InfoLine = "Create a document that shows a information stored in a space using a functional graph";
            InfoTip1 = "Usefull for current state analysis";
            InfoTip2 = "Does not show temporal information";
        }
    }
}
