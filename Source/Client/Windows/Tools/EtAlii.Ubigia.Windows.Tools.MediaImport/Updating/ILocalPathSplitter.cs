namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    internal interface ILocalPathSplitter
    {
        void Split(string localStart, string path, out string last, out string[] rest);
    }
}