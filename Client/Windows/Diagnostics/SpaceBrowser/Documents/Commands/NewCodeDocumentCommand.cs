﻿namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
            ICodeDocumentFactory factory,
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
            Header = "Code";
            Icon = @"\Images\Icons\File-Format-CSharp.png";
            TitleFormat = "Code view {0}";
            InfoLine = "Create a document to interact with a space programmatically";
            InfoTip1 = "Usefull for complex iterative or recursive activities";
            InfoTip2 = "Allows C# code to be tested";
        }
    }
}
