namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IStatusWriter
    {
        void Write(IScriptViewModel viewModel, string message);
    }
}