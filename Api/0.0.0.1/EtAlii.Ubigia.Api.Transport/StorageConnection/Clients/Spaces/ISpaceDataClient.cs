namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISpaceDataClient : IStorageTransportClient
    {
        Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template);
        Task Remove(Guid spaceId);
        Task<Space> Change(Guid spaceId, string spaceName);
        Task<Space> Get(Guid accountId, string spaceName);
        Task<Space> Get(Guid spaceId);
        Task<IEnumerable<Space>> GetAll(Guid accountId);
    }

    public interface ISpaceDataClient<in TTransport> : ISpaceDataClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
