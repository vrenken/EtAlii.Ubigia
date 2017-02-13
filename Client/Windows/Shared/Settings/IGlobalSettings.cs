using System.Collections.ObjectModel;

namespace EtAlii.Ubigia.Client.Windows
{
    public interface IGlobalSettings
    {
        bool AutomaticallySendLogFiles { get; set; }
        string ConsoleTarget { get; set; }
        bool RememberLoginCredentials { get; set; }
        bool StartAutomatically { get; set; }
        ObservableCollection<StorageSettings> Storage { get; }
    }
}