// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System;

public class DefaultContainerProvider : IContainerProvider
{
    private const string EntriesFolderName = "Entries";


    public ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId)
    {
        var paths = new[]
        {
            EntriesFolderName,
            storageId.ToString(),
            accountId.ToString(),
            spaceId.ToString()
        };
        return ContainerIdentifier.FromPaths(paths);
    }

    public ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId, ulong era, ulong period, ulong moment)
    {
        var paths = new[]
        {
            EntriesFolderName,
            storageId.ToString(),
            accountId.ToString(),
            spaceId.ToString(),
            era.ToString(),
            period.ToString(),
            moment.ToString(),
        };
        return ContainerIdentifier.FromPaths(paths);
    }

    public ContainerIdentifier ForEntry(string storageId, string accountId, string spaceId, ulong era, ulong period, ulong moment)
    {
        var paths = new[]
        {
            EntriesFolderName,
            storageId,
            accountId,
            spaceId,
            era.ToString(),
            period.ToString(),
            moment.ToString(),
        };
        return ContainerIdentifier.FromPaths(paths);
    }

    public ContainerIdentifier ForEntry(string storageId, string accountId, string spaceId, string era, string period, string moment)
    {
        var paths = new[]
        {
            EntriesFolderName,
            storageId,
            accountId,
            spaceId,
            era,
            period,
            moment,
        };
        return ContainerIdentifier.FromPaths(paths);
    }

    public ContainerIdentifier ForRoots(Guid spaceId)
    {
        var paths = new[]
        {
            "Roots",
            spaceId.ToString()
        };
        return ContainerIdentifier.FromPaths(paths);
    }


    public ContainerIdentifier ForItems(string folder)
    {
        var paths = new[]
        {
            folder
        };
        return ContainerIdentifier.FromPaths(paths);
    }

    public ContainerIdentifier FromIdentifier(in Identifier id, bool trimTime = false)
    {
        string[] paths;
        if (trimTime)
        {
            paths = new[]
            {
                EntriesFolderName,
                id.Storage.ToString(),
                id.Account.ToString(),
                id.Space.ToString(),
            };
        }
        else
        {
            paths = new[]
            {
                EntriesFolderName,
                id.Storage.ToString(),
                id.Account.ToString(),
                id.Space.ToString(),
                id.Era.ToString(),
                id.Period.ToString(),
                id.Moment.ToString(),
            };
        }

        return ContainerIdentifier.FromPaths(paths);
    }

    public Identifier ToIdentifier(Guid storageId, Guid accountId, Guid spaceId, ContainerIdentifier containerId)
    {
        var paths = containerId.Paths;
        var era = ulong.Parse(paths[4]);
        var period = ulong.Parse(paths[5]);
        var moment = ulong.Parse(paths[6]);
        return Identifier.Create(storageId, accountId, spaceId, era, period, moment);
    }
}
