namespace EtAlii.Servus.Storage
{
    using Microsoft.Experimental.IO;
    using System;
    using System.IO;

    public partial class NtfsContainerStorage : NtfsStorageBase, IContainerStorage
    {
        private void SaveToFolder<T>(T item, string itemName, string folder)
            where T : class
        {
            if (!LongPathDirectory.Exists(folder))
            {
                throw new InvalidOperationException("The provided entry has not been prepared.");
            }

            var fileName = String.Format(Serializer.FileNameFormat, itemName);
            fileName = Path.Combine(folder, fileName);

            Serializer.Serialize(fileName, item);

            if (LongPathHelper.GetLength(fileName) == 0)
            {
                throw new InvalidOperationException("An empty file has been stored.");
            }
        }

        private T LoadFromFolder<T>(string folderName, string itemName)
            where T : class
        {
            T item = null;

            if (LongPathDirectory.Exists(folderName))
            {
                var fileName = String.Format(Serializer.FileNameFormat, itemName);
                fileName = Path.Combine(folderName, fileName);

                if (LongPathFile.Exists(fileName))
                {
                    item = Serializer.Deserialize<T>(fileName);
                }
            }
            return item;
        }
    }
}
