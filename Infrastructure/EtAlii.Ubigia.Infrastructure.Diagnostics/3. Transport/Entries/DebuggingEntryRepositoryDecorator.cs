﻿namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class DebuggingEntryRepositoryDecorator : IEntryRepository
    {
        private readonly IEntryRepository _repository;

        public DebuggingEntryRepositoryDecorator(IEntryRepository entryRepository)
        {
            _repository = entryRepository;
        }

        public IEnumerable<Entry> Get(IEnumerable<Identifier> identifiers, EntryRelation entryRelations = EntryRelation.None)
        {
            var entries = _repository.Get(identifiers, entryRelations);

            foreach (var entry in entries)
            {
                EnsureUniqueComponents(entry);
            }

            return entries;
        }

        public Entry Get(Identifier identifier, EntryRelation entryRelations = EntryRelation.None)
        {
            var entry = _repository.Get(identifier, entryRelations);

            EnsureUniqueComponents(entry);

            return entry;
        }

        public async IAsyncEnumerable<Entry> GetRelated(Identifier identifier, EntryRelation entriesWithRelation, EntryRelation entryRelations = EntryRelation.None)
        {
            var entries = _repository.GetRelated(identifier, entriesWithRelation, entryRelations);

            await foreach(var entry in entries)
            {
                EnsureUniqueComponents(entry);
                yield return entry;
            }
        }

        public Entry Prepare(Guid spaceId)
        {
            var entry = _repository.Prepare(spaceId);
            return entry;
        }

        public Entry Prepare(Guid spaceId, Identifier identifier)
        {
            var entry = _repository.Prepare(spaceId, identifier);
            return entry;
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