namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public class NewProfilingDocumentCommand : NewDocumentCommandBase, INewProfilingDocumentCommand
    {
        public NewProfilingDocumentCommand(IDocumentContext documentContext, IProfilingDocumentFactory factory) 
            : base(documentContext)
        {
            DocumentFactory = factory;
            Header = "Profiling";
            Icon = @"pack://siteoforigin:,,,/Images/Arrow.png";
            TitleFormat = "Profiler view {0}";
            InfoLine = "Create a profiling document";
            InfoTip1 = "Shows profiling details of all API access to a space";
            InfoTip2 = "Useful for advanced query optimization";
        }
    }
}
