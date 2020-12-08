﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class InfrastructureTestHelper
    {
        
        public async Task<IEditableEntry[]> CreateSequence(int count, IInfrastructure infrastructure)
        {
            var space = await CreateSpace(infrastructure).ConfigureAwait(false);
            var createdEntries = new IEditableEntry[count];
            IEditableEntry previousEntry = null;
            for (var i = 0; i < count; i++)
            {
                var createdEntry = (IEditableEntry)await infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
                createdEntries[i] = createdEntry;
                if (previousEntry != null)
                {
                    createdEntry.Previous = Relation.NewRelation(previousEntry.Id);
                }
                infrastructure.Entries.Store(createdEntry);
                previousEntry = createdEntry;
            }
            return createdEntries;
        }

        public async Task<IEditableEntry[]> CreateFirstTypeHierarchy(int count, IInfrastructure infrastructure)
        {
            var space = await CreateSpace(infrastructure).ConfigureAwait(false);
            var createdEntries = new IEditableEntry[count];
            IEditableEntry parentEntry = null;
            for (var i = 0; i < count; i++)
            {
                var createdEntry = (IEditableEntry)await infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
                if (parentEntry != null)
                {
                    createdEntry.Parent = Relation.NewRelation(parentEntry.Id);
                }
                createdEntries[i] = infrastructure.Entries.Store(createdEntry);
                parentEntry = createdEntry;
            }
            return createdEntries;
        }

        public async Task<IEditableEntry[]> CreateSecondTypeHierarchy(int count, IInfrastructure infrastructure)
        {
            var space = await CreateSpace(infrastructure).ConfigureAwait(false);
            var createdEntries = new IEditableEntry[count];
            IEditableEntry parent2Entry = null;
            for (var i = 0; i < count; i++)
            {
                var createdEntry = (IEditableEntry)await infrastructure.Entries.Prepare(space.Id).ConfigureAwait(false);
                if (parent2Entry != null)
                {
                    createdEntry.Parent2 = Relation.NewRelation(parent2Entry.Id);
                }
                createdEntries[i] = infrastructure.Entries.Store(createdEntry);
                parent2Entry = createdEntry;
            }
            return createdEntries;
        }

        public Root CreateRoot()
        {
            return new Root
            {
                Name = Guid.NewGuid().ToString(),
            };
        }

        public async Task<Space> CreateSpace(IInfrastructure infrastructure, bool addToRepository = true)
        {
            var space = new Space
            {
                Id = Guid.Empty,
                AccountId = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
            };

            if (addToRepository)
            {
                space = await infrastructure.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
            }
            return space;
        }
    }
}
