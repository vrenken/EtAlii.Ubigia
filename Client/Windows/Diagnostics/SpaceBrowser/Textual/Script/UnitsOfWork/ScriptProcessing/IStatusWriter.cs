namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public interface IStatusWriter
    {
        void Write(IScriptViewModel viewModel, string message);
    }
}