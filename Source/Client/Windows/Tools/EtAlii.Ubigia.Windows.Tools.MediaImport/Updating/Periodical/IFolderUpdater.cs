namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    internal interface IFolderUpdater
    {
        void Handle(string folder, string localStart, string remoteStart);
    }
}