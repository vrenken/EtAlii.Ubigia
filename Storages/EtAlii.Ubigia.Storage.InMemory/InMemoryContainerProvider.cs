namespace EtAlii.Ubigia.Storage.InMemory
{
    using System;
    using EtAlii.Ubigia.Api;

    public class InMemoryContainerProvider : IContainerProvider
    {
        private const string _entriesFolderName = "Entries";


        public ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId)
        {
            var paths = new string[] 
            {
                _entriesFolderName,
                storageId.ToString(),
                accountId.ToString(),
                spaceId.ToString()
            };
            return ContainerIdentifier.FromPaths(paths);
        }

        public ContainerIdentifier ForEntry(Guid storageId, Guid accountId, Guid spaceId, ulong era, ulong period, ulong moment)
        {
            var paths = new string[] 
            {
                _entriesFolderName,
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
            var paths = new string[] 
            {
                _entriesFolderName,
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
            var paths = new string[] 
            {
                _entriesFolderName,
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
            var paths = new string[] 
            {
                "Roots",
                spaceId.ToString()
            };
            return ContainerIdentifier.FromPaths(paths);
        }


        public ContainerIdentifier ForItems(string folder)
        {
            var paths = new string[] 
            {
                folder
            };
            return ContainerIdentifier.FromPaths(paths);
        }

        public ContainerIdentifier FromIdentifier(Identifier id, bool trimTime = false)
        {
            // C:\Users\{User}\AppData\Local\{Company}\{Product}\{Storage}\Entries\{Storage}\{Account}\{Space}\{Period}\{Moment}
            String[] paths;
            if (trimTime)
            {
                paths = new string[] 
                {
                    _entriesFolderName,
                    id.Storage.ToString(),
                    id.Account.ToString(),
                    id.Space.ToString(),
                };
            }
            else
            {
                paths = new string[] 
                {
                    _entriesFolderName,
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
            var era = UInt64.Parse(paths[4]);
            var period = UInt64.Parse(paths[5]);
            var moment = UInt64.Parse(paths[6]);
            return Identifier.Create(storageId, accountId, spaceId, era, period, moment);
        }
    }
}
