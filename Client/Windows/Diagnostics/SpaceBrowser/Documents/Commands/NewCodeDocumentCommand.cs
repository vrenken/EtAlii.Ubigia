namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewCodeDocumentCommand : NewDocumentCommandBase, INewCodeDocumentCommand
    {
        public NewCodeDocumentCommand(
            IGraphSLScriptContext graphSlScriptContext,
            ICodeDocumentFactory factory,
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
            Header = "Code";
            Icon = @"pack://siteoforigin:,,,/Images/File-Format-CSharp.png";
            TitleFormat = "Code view {0}";
            InfoLine = "Create a document to interact with a space programmatically";
            InfoTip1 = "Useful for complex iterative or recursive activities";
            InfoTip2 = "Allows C# code to be tested";
        }
    }
}
