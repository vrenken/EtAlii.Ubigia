// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Logical;

internal class AccountInitializer : IAccountInitializer
{
    private readonly ILogicalContext _context;
    private readonly ISpaceInitializer _spaceInitializer;

    public AccountInitializer(
        ILogicalContext context,
        ISpaceInitializer spaceInitializer)
    {
        _context = context;
        _spaceInitializer = spaceInitializer;
    }

    /// <inheritdoc />
    public async Task Initialize(Account account, AccountTemplate template)
    {
        var accountId = account.Id;
        var spaces = _context.Spaces.GetAll();

        var hasSpace = await spaces
            .AnyAsync(space => space.AccountId == accountId)
            .ConfigureAwait(false);
        if (hasSpace)
        {
            throw new InvalidOperationException("The account already contains a space");
        }

        var duplicates = template.SpacesToCreate
            .GroupBy(s => s.Name)
            .Where(g => g.Count() > 1)
            .Select(y => y.Key);

        if (duplicates.Any())
        {
            throw new InvalidOperationException("The account template contains duplicate space definitions");
        }

        // And finally let's add the spaces that
        foreach (var spaceToCreate in template.SpacesToCreate)
        {
            var (addedSpace, isAdded) = await _context.Spaces
                .Add(new Space { AccountId = accountId, Name = spaceToCreate.Name }, spaceToCreate)
                .ConfigureAwait(false);
            if (isAdded)
            {
                await _spaceInitializer
                    .Initialize(addedSpace, spaceToCreate)
                    .ConfigureAwait(false);
            }
        }
    }
}
