namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    public interface IProcessConfigurationGetter
    {
        EntryConfiguration GetConfiguration(ProcessReason processReason);
    }
}
