﻿namespace EtAlii.Ubigia.Persistence
{
    using System.Linq;

    internal class LatestEntryGetter : ILatestEntryGetter
    {
        private readonly IImmutableFolderManager _folderManager;

        public LatestEntryGetter(IImmutableFolderManager folderManager)
        {
            _folderManager = folderManager;
        }

        public string GetLatestEntry(string folder, ulong delta)
        {
            ulong count = 0;
            if (_folderManager.Exists(folder))
            {
                foreach (var subFolder in _folderManager.EnumerateDirectories(folder))
                {
                    var part = subFolder.Split('\\').Last();
                    var item = ulong.Parse(part);
                    count = count < item ? item : count;
                }

                count += delta;
            }
            return count.ToString();
        }
    }
}
