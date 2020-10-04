namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Threading.Tasks;

    public interface IEntryResolver
    {
        Task<Entry> Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry);
    }
}