namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.ObjectModel;

    public interface IExecutionStatusProvider
    {
        ObservableCollection<string> ExecutionStatus { get; }
    }
}