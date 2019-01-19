namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class NewLogicalGraphDocumentCommand : NewDocumentCommandBase, INewLogicalGraphDocumentCommand
    {
        public NewLogicalGraphDocumentCommand(IDocumentContext documentContext, ILogicalGraphDocumentFactory factory) 
            : base(documentContext)
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
