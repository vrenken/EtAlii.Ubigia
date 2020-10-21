﻿namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal class RootContext : IRootContext
    {
        public event Action<Guid> Added = delegate { };
        public event Action<Guid> Changed = delegate { };
        public event Action<Guid> Removed = delegate { };

        private readonly IDataConnection _connection;

        public RootContext(IDataConnection connection)
        {
            if (connection == null) return; // In the new setup the LogicalContext and IDataConnection are instantiated at the same time.
            _connection = connection;
            _connection.Roots.Notifications.Added += OnAdded;
            _connection.Roots.Notifications.Changed += OnChanged;
            _connection.Roots.Notifications.Removed += OnRemoved;
        }

        public async Task<Root> Add(string name) 
        {
            return await _connection.Roots.Data.Add(name);
        }

        public async Task Remove(Guid id)
        {
            await _connection.Roots.Data.Remove(id);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            return await _connection.Roots.Data.Change(rootId, rootName);
        }

        public async Task<Root> Get(string rootName)
        {
            return await _connection.Roots.Data.Get(rootName);
        }

        public async Task<Root> Get(Guid rootId)
        {
            return await _connection.Roots.Data.Get(rootId);
        }

        public IAsyncEnumerable<Root> GetAll()
        {
            return _connection.Roots.Data.GetAll();
        }


        private void OnAdded(Guid id)
        {
            Added(id);
        }

        private void OnChanged(Guid id)
        {
            Changed(id);
        }

        private void OnRemoved(Guid id)
        {
            Removed(id);
        }
    }
}
