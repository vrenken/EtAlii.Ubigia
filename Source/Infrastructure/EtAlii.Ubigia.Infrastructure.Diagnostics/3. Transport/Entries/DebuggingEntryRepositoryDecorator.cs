// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class DebuggingEntryRepositoryDecorator : IEntryRepository
    {
        private readonly IEntryRepository _repository;

        public DebuggingEntryRepositoryDecorator(IEntryRepository entryRepository)
        {
            _repository = entryRepository;
        }

        public async IAsyncEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelations entryRelations = EntryRelations.None)
        {
            var items = _repository.Get(identifiers, entryRelations);
            await foreach (var item in items.ConfigureAwait(false))
            {
                EnsureUniqueComponents(item);
                yield return item;
            }
        }

        public async Task<Entry> Get(Identifier identifier, EntryRelations entryRelations = EntryRelations.None)
        {
            var entry = await _repository.Get(identifier, entryRelations).ConfigureAwait(false);

            EnsureUniqueComponents(entry);

            return entry;
        }

        public async IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelations entriesWithRelation, EntryRelations entryRelations = EntryRelations.None)
        {
            var items = _repository.GetRelated(identifier, entriesWithRelation, entryRelations);
            await foreach(var item in items)
            {
                EnsureUniqueComponents(item);
                yield return item;
            }
        }

        public Task<Entry> Prepare(Guid spaceId)
        {
            return _repository.Prepare(spaceId);
        }

        public Task<Entry> Prepare(Guid spaceId, Identifier identifier)
        {
            return _repository.Prepare(spaceId, identifier);
        }

        public Entry Store(Entry entry)
        {
            EnsureUniqueComponents(entry);

            var storedEntry = _repository.Store(entry);

            EnsureUniqueComponents(entry);

            return storedEntry;
        }

        public Entry Store(IEditableEntry entry)
        {
            EnsureUniqueComponents((Entry)entry);

            var storedEntry = _repository.Store(entry);

            EnsureUniqueComponents((Entry)entry);

            return storedEntry;
        }

        private void EnsureUniqueComponents(Entry entry)
        {
            EnsureUniqueComponents(entry.Children, "Children");
            EnsureUniqueComponents(entry.Children2, "Children2");
            EnsureUniqueComponents(entry.Updates, "Updates");
        }

        private void EnsureUniqueComponents(IEnumerable<Relation> allRelations, string name)
        {
            var uniqueRelations = allRelations.Distinct();
            if (allRelations.Count() != uniqueRelations.Count())
            {
                var doubleRelations = allRelations.Except(uniqueRelations);
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("The {0} property contains duplicate relations:{1}", name, Environment.NewLine);
                foreach (var doubleRelation in doubleRelations)
                {
                    messageBuilder.AppendLine(doubleRelation.Id.ToString());
                }
                var exception = new InvalidDataException(messageBuilder.ToString());
                exception.Data["Double relations"] = doubleRelations.ToArray();
                throw exception;
            }
        }
    }
}
