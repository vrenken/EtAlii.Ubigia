namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class InfrastructureTestHelper
    {
        public static async Task<IEditableEntry[]> CreateSequence(int count, IInfrastructure infrastructure)
        {
            var space = await CreateSpace(infrastructure);
            var createdEntries = new IEditableEntry[count];
            IEditableEntry previousEntry = null;
            for (int i = 0; i < count; i++)
            {
                var createdEntry = (IEditableEntry)infrastructure.Entries.Prepare(space.Id);
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

        public static async Task<IEditableEntry[]> CreateFirstTypeHierarchy(int count, IInfrastructure infrastructure)
        {
            var space = await CreateSpace(infrastructure);
            var createdEntries = new IEditableEntry[count];
            IEditableEntry parentEntry = null;
            for (int i = 0; i < count; i++)
            {
                var createdEntry = (IEditableEntry)infrastructure.Entries.Prepare(space.Id);
                if (parentEntry != null)
                {
                    createdEntry.Parent = Relation.NewRelation(parentEntry.Id);
                }
                createdEntries[i] = infrastructure.Entries.Store(createdEntry);
                parentEntry = createdEntry;
            }
            return createdEntries;
        }

        public static async Task<IEditableEntry[]> CreateSecondTypeHierarchy(int count, IInfrastructure infrastructure)
        {
            var space = await CreateSpace(infrastructure);
            var createdEntries = new IEditableEntry[count];
            IEditableEntry parent2Entry = null;
            for (int i = 0; i < count; i++)
            {
                var createdEntry = (IEditableEntry)infrastructure.Entries.Prepare(space.Id);
                if (parent2Entry != null)
                {
                    createdEntry.Parent2 = Relation.NewRelation(parent2Entry.Id);
                }
                createdEntries[i] = infrastructure.Entries.Store(createdEntry);
                parent2Entry = createdEntry;
            }
            return createdEntries;
        }

        public static Root CreateRoot()
        {
            return new Root
            {
                Name = Guid.NewGuid().ToString(),
            };
        }

        public static async Task<Space> CreateSpace(IInfrastructure infrastructure, bool addToRepository = true)
        {
            var space = new Space
            {
                Id = Guid.Empty,
                AccountId = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
            };

            if (addToRepository)
            {
                space = await infrastructure.Spaces.Add(space, SpaceTemplate.Data);
            }
            return space;
        }

    }
}
