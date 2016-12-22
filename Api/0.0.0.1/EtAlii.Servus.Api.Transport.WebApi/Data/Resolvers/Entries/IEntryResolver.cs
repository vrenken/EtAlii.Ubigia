namespace EtAlii.Servus.Api.Management
{
    using System.Threading.Tasks;

    public interface IEntryResolver
    {
        Task<Entry> Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry);
    }
}