namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Microsoft.Experimental.IO;
    using System;
    using System.Collections.Generic;

    public class LatestEntryGetter : ILatestEntryGetter
    {
        public string GetLatestEntry(string folder, UInt64 delta)
        {
            UInt64 count = 0;
            if (LongPathDirectory.Exists(folder))
            {
                foreach (var subFolder in LongPathDirectory.EnumerateDirectories(folder))
                {
                    count += 1;
                }

                count -= 1;
                count += delta;
            }
            return count.ToString();
        }
    }
}
