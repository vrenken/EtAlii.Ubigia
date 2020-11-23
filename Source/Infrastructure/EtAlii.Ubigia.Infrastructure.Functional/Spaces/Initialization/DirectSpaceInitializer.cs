﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class DirectSpaceInitializer : ISpaceInitializer
    {
        private readonly ILogicalContext _context;

        public DirectSpaceInitializer(ILogicalContext context)
        {
            _context = context;
        }

        public async Task Initialize(Space space, SpaceTemplate template)
        {
            var storageId = _context.Storages.GetLocal().Id;
            var accountId = space.AccountId;
            var spaceId = space.Id;

            var hasRoots = await _context.Roots
                .GetAll(spaceId)
                .AnyAsync();

            if (hasRoots)
            {
                throw new InvalidOperationException("The space already contains roots");
            }

            var rootsToCreate = template.RootsToCreate;
            var spaceIdentifier = Identifier.NewIdentifier(storageId, accountId, spaceId);

            var rootEntryCount = rootsToCreate.Length;
            var previousIdentifier = Identifier.Empty;
            var rootEntries = new IEditableEntry[rootEntryCount];
            var tailIdentifier = Identifier.Empty;
            for (var i = 0; i < rootEntryCount; i++)
            {
                var newId = Identifier.NewIdentifier(spaceIdentifier, 0, 0, (ulong)i);
                var entry = (IEditableEntry)await _context.Entries.Prepare(spaceId, newId).ConfigureAwait(false);
                entry.Type = rootsToCreate[i];
                if (i == 0)
                {
                    tailIdentifier = entry.Id;
                }
                else
                {
                    entry.Previous = Relation.NewRelation(previousIdentifier); // All roots are sequenced next to each other.
                    entry.Parent = Relation.NewRelation(tailIdentifier); // Everything is child of the tail.
                }
                _context.Entries.Store(entry);
                rootEntries[i] = entry;
                previousIdentifier = rootEntries[i].Id;
            }

            foreach (var rootEntry in rootEntries)
            {
                await _context.Roots.Add(spaceId, new Root { Name = rootEntry.Type, Identifier = rootEntry.Id }).ConfigureAwait(false);
            }
        }
    }
}