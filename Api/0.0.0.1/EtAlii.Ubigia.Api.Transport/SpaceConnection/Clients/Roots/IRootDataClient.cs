namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRootDataClient : ISpaceTransportClient
    {
        Task<Root> Add(string name);
        Task Remove(Guid id);
        Task<Root> Change(Guid rootId, string rootName);
        Task<Root> Get(string rootName);
        Task<Root> Get(Guid rootId);
        Task<IEnumerable<Root>> GetAll();
    }

    public interface IRootDataClient<in TTransport> : IRootDataClient, ISpaceTransportClient<TTransport>
        where TTransport: ISpaceTransport
    {
    }
}
