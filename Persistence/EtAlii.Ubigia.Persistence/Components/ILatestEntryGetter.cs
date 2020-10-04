namespace EtAlii.Ubigia.Persistence
{
    internal interface ILatestEntryGetter
    {
        string GetLatestEntry(string folder, ulong delta);
    }
}
