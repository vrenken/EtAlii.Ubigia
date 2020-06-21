namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    public interface IEntryResolver
    {
        Task<Entry> Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry);
    }
}