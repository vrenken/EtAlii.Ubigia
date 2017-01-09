namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.IO;

    internal class DirectoryHelper
    {
        public bool IsDirectory(string item)
        {
            return File.GetAttributes(item).HasFlag(FileAttributes.Directory);
        }

        public bool IsFile(string item)
        {
            return !File.GetAttributes(item).HasFlag(FileAttributes.Directory);
        }
    }
}
