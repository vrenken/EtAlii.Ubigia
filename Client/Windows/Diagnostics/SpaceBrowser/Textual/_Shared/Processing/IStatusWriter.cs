namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    public interface IStatusWriter
    {
        void Write(IExecutionStatusProvider statusProvider, string message);
    }
}