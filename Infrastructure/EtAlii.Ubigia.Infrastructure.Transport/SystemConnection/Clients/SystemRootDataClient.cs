namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class SystemRootDataClient : SystemSpaceClientBase, IRootDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemRootDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public Task<Root> Add(string name)
        {
            var root = new Root
            {
                Name = name,
            };
            var result = _infrastructure.Roots.Add(Connection.Space.Id, root);
            return Task.FromResult(result);
        }

        public Task Remove(Guid id)
        {
            _infrastructure.Roots.Remove(Connection.Space.Id, id);
            return Task.CompletedTask;
        }

        public Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            var result = _infrastructure.Roots.Update(Connection.Space.Id, rootId, root);
            return Task.FromResult(result);
        }

        public Task<Root> Get(string rootName)
        {
            var result = _infrastructure.Roots.Get(Connection.Space.Id, rootName);
            return Task.FromResult(result);
        }

        public Task<Root> Get(Guid rootId)
        {
            var result = _infrastructure.Roots.Get(Connection.Space.Id, rootId);
            return Task.FromResult(result);
        }

        public IAsyncEnumerable<Root> GetAll()
        {
            var result = _infrastructure.Roots.GetAll(Connection.Space.Id);
            return result.ToAsyncEnumerable(); // TODO: ASyncEnumerable
        }
    }
}
