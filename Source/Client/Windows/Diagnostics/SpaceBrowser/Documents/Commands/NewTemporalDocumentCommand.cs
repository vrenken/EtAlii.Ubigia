namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class NewTemporalDocumentCommand : NewDocumentCommandBase, INewTemporalDocumentCommand
    {
        public NewTemporalDocumentCommand(IDocumentContext documentContext, ITemporalDocumentFactory factory) 
            : base(documentContext)
        {
            DocumentFactory = factory;
            Header = "Temporal";
            Icon = @"pack://siteoforigin:,,,/Images/Clock-01.png";
            TitleFormat = "Temporal view {0}";
            InfoLine = "Create a document to show information stored in a space temporal";
            InfoTip1 = "Useful for temporal analysis";
            InfoTip2 = null;
        }
    }
}
