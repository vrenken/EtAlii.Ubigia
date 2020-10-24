namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    internal interface IDirectoryHelper
    {
        bool IsDirectory(string item);
        bool IsFile(string item);
    }
}