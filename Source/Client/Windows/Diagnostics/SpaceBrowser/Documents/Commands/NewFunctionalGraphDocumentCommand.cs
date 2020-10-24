namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class NewFunctionalGraphDocumentCommand : NewDocumentCommandBase, INewFunctionalGraphDocumentCommand
    {
        public NewFunctionalGraphDocumentCommand(IDocumentContext documentContext, IFunctionalGraphDocumentFactory factory) 
            : base(documentContext)
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
