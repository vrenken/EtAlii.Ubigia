namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IStatusWriter
    {
        void Write(IGraphScriptLanguageViewModel viewModel, string message);
    }
}