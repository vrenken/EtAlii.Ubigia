namespace EtAlii.Servus.Storage
{
    using Microsoft.Experimental.IO;
    using System.IO;

    public static class LongPathHelper
    {

        public static void Delete(string folder)
        {
            var subFolders = LongPathDirectory.EnumerateDirectories(folder);
//                                              .ToArray();
            foreach (var subFolder in subFolders)
            {
                Delete(subFolder);
            }

            var files = LongPathDirectory.EnumerateFiles(folder);
//                                         .ToArray();
            foreach (var file in files)
            {
                LongPathFile.Delete(file);
            }

            LongPathDirectory.Delete(folder);
        }

        public static void Create(string folder)
        {
            var parentFolder = Path.GetDirectoryName(folder);
            if (!LongPathDirectory.Exists(parentFolder))
            {
                Create(parentFolder);
            }
            LongPathDirectory.Create(folder);
        }

        public static long GetLength(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Length;
        }
    }
}
