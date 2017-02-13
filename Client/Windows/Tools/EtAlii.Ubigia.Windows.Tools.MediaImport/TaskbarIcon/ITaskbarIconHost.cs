namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    internal interface ITaskbarIconHost
    {
        ITaskbarIcon TaskbarIcon { get; }

        void Start();
    }
}