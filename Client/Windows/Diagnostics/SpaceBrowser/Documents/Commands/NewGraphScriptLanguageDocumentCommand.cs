namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class NewGraphScriptLanguageDocumentCommand : NewDocumentCommandBase, INewGraphScriptLanguageDocumentCommand 
    {
        public NewGraphScriptLanguageDocumentCommand(IDocumentContext documentContext, IGraphScriptLanguageDocumentFactory factory) 
            : base(documentContext)
        {
            DocumentFactory = factory;
            Header = "Script";
            Icon = @"pack://siteoforigin:,,,/Images/File-Format-GraphQuery.png";
            TitleFormat = "GraphSL view {0}";
            InfoLine = "Create a document to invoke GSL scripts on a space";
            InfoTip1 = "Allows execution scripts written in the GSL language";
            InfoTip2 = "Useful for advanced space operations";
        }
    }
}
