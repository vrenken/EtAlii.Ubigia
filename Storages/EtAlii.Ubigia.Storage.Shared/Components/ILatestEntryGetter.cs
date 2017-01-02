namespace EtAlii.Ubigia.Storage
{
    using System;

    internal interface ILatestEntryGetter
    {
        string GetLatestEntry(string folder, UInt64 delta);
    }
}
