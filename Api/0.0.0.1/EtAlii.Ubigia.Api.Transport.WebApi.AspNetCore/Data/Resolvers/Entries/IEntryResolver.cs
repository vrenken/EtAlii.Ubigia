namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System.Threading.Tasks;

    public interface IEntryResolver
    {
        Task<Entry> Get(IEntryInfoProvider entryInfoProvider, Entry currentEntry);
    }
}