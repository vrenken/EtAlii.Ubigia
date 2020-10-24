namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class NewSequentialDocumentCommand : NewDocumentCommandBase, INewSequentialDocumentCommand
    {
        public NewSequentialDocumentCommand(IDocumentContext documentContext, ISequentialDocumentFactory factory) 
            : base(documentContext)
        {
            DocumentFactory = factory;
            Header = "Sequential";
            Icon = @"pack://siteoforigin:,,,/Images/View-Details.png";
            TitleFormat = "Sequential view {0}";
            InfoLine = "Create a document to show information stored in a space sequentially";
            InfoTip1 = "Useful for order analysis";
            InfoTip2 = "Does not show temporal information";
        }
    }
}
