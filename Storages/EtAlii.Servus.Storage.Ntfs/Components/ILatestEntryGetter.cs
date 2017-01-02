namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;

    public interface ILatestEntryGetter
    {
        string GetLatestEntry(string folder, UInt64 delta);
    }
}
