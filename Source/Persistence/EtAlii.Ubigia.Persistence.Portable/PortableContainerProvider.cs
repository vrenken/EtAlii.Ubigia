// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using System;

    public class PortableContainerProvider : IContainerProvider
    {
        private const string EntriesFolderName = "Entries";

        public ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId)
        {
            var paths = new[]
            {
                EntriesFolderName,
                Base36Convert.ToString(storageId),
                Base36Convert.ToString(accountId),
                Base36Convert.ToString(spaceId)
            };
            return ContainerIdentifier.FromPaths(paths);
        }

        public ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId, ulong era, ulong period, ulong moment)
        {
            var paths = new[]
            {
                EntriesFolderName,
                Base36Convert.ToString(storageId),
                Base36Convert.ToString(accountId),
                Base36Convert.ToString(spaceId),
                Base36Convert.ToString(era),
                Base36Convert.ToString(period),
                Base36Convert.ToString(moment),
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
                Base36Convert.ToString(era),
                Base36Convert.ToString(period),
                Base36Convert.ToString(moment),
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
                Base36Convert.ToString(spaceId)
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
            // C:\Users\[User]\AppData\Local\[Company]\[Product]\[Storage]\Entries\[Storage]\[Account]\[Space]\[Period]\[Moment]
            string[] paths;
            if (trimTime)
            {
                paths = new[]
                {
                    EntriesFolderName,
                    Base36Convert.ToString(id.Storage),
                    Base36Convert.ToString(id.Account),
                    Base36Convert.ToString(id.Space),
                };
            }
            else
            {
                paths = new[]
                {
                    EntriesFolderName,
                    Base36Convert.ToString(id.Storage),
                    Base36Convert.ToString(id.Account),
                    Base36Convert.ToString(id.Space),
                    Base36Convert.ToString(id.Era),
                    Base36Convert.ToString(id.Period),
                    Base36Convert.ToString(id.Moment),
                };
            }

            return ContainerIdentifier.FromPaths(paths);
        }

        public Identifier ToIdentifier(Guid storageId, Guid accountId, Guid spaceId, ContainerIdentifier containerId)
        {
            var paths = containerId.Paths;
            var era = Base36Convert.ToUInt64(paths[4]);
            var period = Base36Convert.ToUInt64(paths[5]);
            var moment = Base36Convert.ToUInt64(paths[6]);
            return Identifier.Create(storageId, accountId, spaceId, era, period, moment);
        }
    }
}
