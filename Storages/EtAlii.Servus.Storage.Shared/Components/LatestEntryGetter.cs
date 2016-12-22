namespace EtAlii.Servus.Storage
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Fabric;

    internal class LatestEntryGetter : ILatestEntryGetter
    {
        private readonly IImmutableFolderManager _folderManager;

        public LatestEntryGetter(IImmutableFolderManager folderManager)
        {
            _folderManager = folderManager;
        }

        public string GetLatestEntry(string folder, UInt64 delta)
        {
            UInt64 count = 0;
            if (_folderManager.Exists(folder))
            {
                foreach (var subFolder in _folderManager.EnumerateDirectories(folder))
                {
                    var part = subFolder.Split('\\').Last();
                    var item = UInt64.Parse(part);
                    count = count < item ? item : count;
                    //count += 1;
                }

                //count -= 1;
                count += delta;
            }
            return count.ToString();
        }
    }
}
