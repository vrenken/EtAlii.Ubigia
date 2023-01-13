// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Functional;

public class InfrastructureTestHelper
{
    public async Task<IEditableEntry[]> CreateSequence(int count, IFunctionalContext functionalContext)
    {
        var space = await CreateSpace(functionalContext).ConfigureAwait(false);
        var createdEntries = new IEditableEntry[count];
        IEditableEntry previousEntry = null;
        for (var i = 0; i < count; i++)
        {
            var createdEntry = (IEditableEntry)await functionalContext.Entries.Prepare(space.Id).ConfigureAwait(false);
            createdEntries[i] = createdEntry;
            if (previousEntry != null)
            {
                createdEntry.Previous = Relation.NewRelation(previousEntry.Id);
            }
            await functionalContext.Entries.Store(createdEntry).ConfigureAwait(false);
            previousEntry = createdEntry;
        }
        return createdEntries;
    }

    public async Task<IEditableEntry[]> CreateFirstTypeHierarchy(int count, IFunctionalContext functionalContext)
    {
        var space = await CreateSpace(functionalContext).ConfigureAwait(false);
        var createdEntries = new IEditableEntry[count];
        IEditableEntry parentEntry = null;
        for (var i = 0; i < count; i++)
        {
            var createdEntry = (IEditableEntry)await functionalContext.Entries.Prepare(space.Id).ConfigureAwait(false);
            if (parentEntry != null)
            {
                createdEntry.Parent = Relation.NewRelation(parentEntry.Id);
            }
            createdEntries[i] = await functionalContext.Entries.Store(createdEntry).ConfigureAwait(false);
            parentEntry = createdEntry;
        }
        return createdEntries;
    }

    public async Task<IEditableEntry[]> CreateSecondTypeHierarchy(int count, IFunctionalContext functionalContext)
    {
        var space = await CreateSpace(functionalContext).ConfigureAwait(false);
        var createdEntries = new IEditableEntry[count];
        IEditableEntry parent2Entry = null;
        for (var i = 0; i < count; i++)
        {
            var createdEntry = (IEditableEntry)await functionalContext.Entries.Prepare(space.Id).ConfigureAwait(false);
            if (parent2Entry != null)
            {
                createdEntry.Parent2 = Relation.NewRelation(parent2Entry.Id);
            }
            createdEntries[i] = await functionalContext.Entries.Store(createdEntry).ConfigureAwait(false);
            parent2Entry = createdEntry;
        }
        return createdEntries;
    }

    public Root CreateRoot()
    {
        return new()
        {
            Name = Guid.NewGuid().ToString(),
            Type = RootType.Text,
        };
    }

    public async Task<Space> CreateSpace(IFunctionalContext functionalContext, bool addToRepository = true)
    {
        var space = new Space
        {
            Id = Guid.Empty,
            AccountId = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
        };

        if (addToRepository)
        {
            space = await functionalContext.Spaces.Add(space, SpaceTemplate.Data).ConfigureAwait(false);
        }
        return space;
    }
}
