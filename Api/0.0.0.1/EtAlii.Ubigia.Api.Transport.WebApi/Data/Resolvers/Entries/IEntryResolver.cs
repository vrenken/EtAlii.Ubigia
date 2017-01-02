namespace EtAlii.Ubigia.Api.Management
{
    using System.Threading.Tasks;

    public interface IEntryResolver
    {
        Task<Entry> Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry);
    }
}