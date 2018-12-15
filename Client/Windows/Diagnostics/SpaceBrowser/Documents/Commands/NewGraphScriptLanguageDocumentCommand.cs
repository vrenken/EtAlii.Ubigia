namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewGraphScriptLanguageDocumentCommand : NewDocumentCommandBase, INewGraphScriptLanguageDocumentCommand 
    {
        public NewGraphScriptLanguageDocumentCommand(IDocumentContext documentContext, IGraphScriptLanguageDocumentFactory factory) 
            : base(documentContext)
        {
            DocumentFactory = factory;
            Header = "Script";
            Icon = @"pack://siteoforigin:,,,/Images/File-Format-GraphQuery.png";
            TitleFormat = "Script view {0}";
            InfoLine = "Create a document to invoke scripts on a space";
            InfoTip1 = "Allows execution scripts written in the GSL language";
            InfoTip2 = "Useful for advanced space operations";
        }
    }
}
