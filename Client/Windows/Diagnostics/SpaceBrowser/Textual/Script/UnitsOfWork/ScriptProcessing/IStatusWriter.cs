namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    public interface IStatusWriter
    {
        void Write(ScriptViewModel viewModel, string message);
    }
}