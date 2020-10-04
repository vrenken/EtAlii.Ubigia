namespace EtAlii.Ubigia.Persistence
{
    using System;

    internal interface ILatestEntryGetter
    {
        string GetLatestEntry(string folder, UInt64 delta);
    }
}
