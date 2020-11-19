namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IEntryPreparer
    {
        Task<Entry> Prepare(Guid spaceId);
        Task<Entry> Prepare(Guid spaceId, Identifier id);
    }
}