namespace EtAlii.Servus.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;

    internal class SystemRootDataClient : SystemSpaceClientBase, IRootDataClient
    {
        private readonly IInfrastructure _infrastructure;

        public SystemRootDataClient(IInfrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }

        public async Task<Root> Add(string name)
        {
            var root = new Root
            {
                Name = name,
            };
            var result = _infrastructure.Roots.Add(Connection.Space.Id, root);
            return await Task.FromResult(result);
        }

        public async Task Remove(Guid id)
        {
            await Task.Run(() => { _infrastructure.Roots.Remove(Connection.Space.Id, id); });
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            var root = new Root
            {
                Id = rootId,
                Name = rootName,
            };

            var result = _infrastructure.Roots.Update(Connection.Space.Id, rootId, root);
            return await Task.FromResult(result);
        }

        public async Task<Root> Get(string rootName)
        {
            var result = _infrastructure.Roots.Get(Connection.Space.Id, rootName);
            return await Task.FromResult(result);
        }

        public async Task<Root> Get(Guid rootId)
        {
            var result = _infrastructure.Roots.Get(Connection.Space.Id, rootId);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            var result = _infrastructure.Roots.GetAll(Connection.Space.Id);
            return await Task.FromResult(result);
        }
    }
}
