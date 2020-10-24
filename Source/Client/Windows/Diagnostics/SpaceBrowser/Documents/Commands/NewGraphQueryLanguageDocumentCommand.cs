namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class NewGraphQueryLanguageDocumentCommand : NewDocumentCommandBase, INewGraphQueryLanguageDocumentCommand
    {
        public NewGraphQueryLanguageDocumentCommand(IDocumentContext documentContext, IGraphQueryLanguageDocumentFactory factory) 
            : base(documentContext)
        {
            DocumentFactory = factory;
            Header = "GraphQL";
            Icon = @"pack://siteoforigin:,,,/Images/File-Format-GraphQuery.png";
            TitleFormat = "GraphQL view {0}";
            InfoLine = "Create a document to invoke scripts on a space";
            InfoTip1 = "Allows execution queries that adhere to the GraphQL definition";
            InfoTip2 = "Useful for information discovery";
        }
    }
}
