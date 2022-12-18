// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

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

    /// <inheritdoc />
    public async Task Initialize(Space space, SpaceTemplate template)
    {
        var storage = await _context.Storages
            .GetLocal()
            .ConfigureAwait(false);
        var storageId = storage.Id;
        var accountId = space.AccountId;
        var spaceId = space.Id;

        var hasRoots = await _context.Roots
            .GetAll(spaceId)
            .AnyAsync()
            .ConfigureAwait(false);

        if (hasRoots)
        {
            throw new InvalidOperationException("The space already contains roots");
        }

        var rootsToCreate = template.RootsToCreate;
        var spaceIdentifier = Identifier.NewIdentifier(storageId, accountId, spaceId);

        var previousIdentifier = Identifier.Empty;
        var tailIdentifier = Identifier.Empty;
        for(var i = 0; i< rootsToCreate.Length; i++)
        {
            var rootTemplate = rootsToCreate[i];
            var newId = Identifier.NewIdentifier(spaceIdentifier, 0, 0, (ulong)i);
            var entry = (IEditableEntry)await _context.Entries.Prepare(spaceId, newId).ConfigureAwait(false);
            entry.Type = rootTemplate.Name;
            if (i == 0)
            {
                tailIdentifier = entry.Id;
            }
            else
            {
                entry.Previous = Relation.NewRelation(previousIdentifier); // All roots are sequenced next to each other.
                entry.Parent = Relation.NewRelation(tailIdentifier); // Everything is child of the tail.
            }
            await _context.Entries.Store(entry).ConfigureAwait(false);
            previousIdentifier = entry.Id;

            var root = new Root
            {
                // RCI2022: We want to make roots case insensitive.
                Name = rootTemplate.Name.ToUpper(),
                Type = rootTemplate.Type,
                Identifier = entry.Id
            };
            await _context.Roots
                .Add(spaceId, root)
                .ConfigureAwait(false);
        }
    }
}
