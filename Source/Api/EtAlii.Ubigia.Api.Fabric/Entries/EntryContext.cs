// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    internal class EntryContext : IEntryContext
    {
        private readonly IDataConnection _connection;

        internal EntryContext(IDataConnection connection)
        {
            if (connection == null) return; // In the new setup the LogicalContext and IDataConnection are instantiated at the same time.
            _connection = connection;
            _connection.Entries.Notifications.Prepared += OnPrepared;
            _connection.Entries.Notifications.Stored += OnStored;
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await _connection.Entries.Data.Prepare().ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            return await _connection.Entries.Data.Change(entry, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Get(Root root, ExecutionScope scope)
        {
            return await _connection.Entries.Data.Get(root, scope, EntryRelation.All).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Get(Identifier identifier, ExecutionScope scope)
        {
            return await _connection.Entries.Data.Get(identifier, scope, EntryRelation.All).ConfigureAwait(false);
        }

        public IAsyncEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers, ExecutionScope scope)
        {
            return _connection.Entries.Data.Get(identifiers, scope, EntryRelation.All);
        }

        public IAsyncEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelation relations, ExecutionScope scope)
        {
            return _connection.Entries.Data.GetRelated(identifier, relations, scope, EntryRelation.All);
        }


        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

        private void OnPrepared(Identifier identifier)
        {
            Prepared(identifier);
        }

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }
    }
}
