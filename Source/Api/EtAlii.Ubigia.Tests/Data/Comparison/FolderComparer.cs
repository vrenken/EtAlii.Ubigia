namespace EtAlii.Ubigia.Tests;

using System.IO;

public class FolderComparer
{
    private readonly FileComparer _fileComparer;

    public FolderComparer(FileComparer fileComparer)
    {
        _fileComparer = fileComparer;
    }

    public bool FoldersAreEqual(string expectedFolderName, string actualFolderName)
    {
        if (!Directory.Exists(expectedFolderName))
        {
            return false;
        }
        if(!Directory.Exists(actualFolderName))
        {
            return false;
        }

        var expectedSubFolders = Directory.GetDirectories(expectedFolderName);
        var actualSubFolders = Directory.GetDirectories(actualFolderName);

        if (expectedSubFolders.Length != actualSubFolders.Length)
        {
            return false;
        }

        for (var i = 0; i < expectedSubFolders.Length; i++)
        {
            var expectedSubFolder = Path.GetFileName(expectedSubFolders[i]);
            var actualSubFolder = Path.GetFileName(actualSubFolders[i]);

            if (expectedSubFolder != actualSubFolder)
            {
                return false;
            }

            FoldersAreEqual(Path.Combine(expectedFolderName, expectedSubFolder), Path.Combine(actualFolderName, actualSubFolder));
        }

        var expectedFiles = Directory.GetFiles(expectedFolderName);
        var actualFiles = Directory.GetFiles(actualFolderName);

        if (expectedFiles.Length != actualFiles.Length)
        {
            return false;
        }

        for (var i = 0; i < expectedSubFolders.Length; i++)
        {
            var expectedFile = Path.GetFileName(expectedFiles[i]);
            var actualFile = Path.GetFileName(actualFiles[i]);

            if (expectedFile != actualFile)
            {
                return false;
            }
            if(!_fileComparer.AreEqual(Path.Combine(expectedFolderName, expectedFile), Path.Combine(actualFolderName, actualFile)))
            {
                return false;
            }
        }
        return true;
    }
}
