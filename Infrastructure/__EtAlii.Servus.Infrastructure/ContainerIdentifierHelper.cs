namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Storage;
    using System;

    public class ContainerIdentifierHelper
    {
        public static ContainerIdentifier FromIdentifier(Identifier id, bool trimTime = false)
        {
            // C:\Users\{User}\AppData\Local\{Company}\{Product}\{Storage}\Entries\{Storage}\{Account}\{Space}\{Period}\{Moment}
            String[] paths;
            if (trimTime)
            {
                paths = new string[] 
                {
                    "Entries",
                    id.Storage.ToString(),
                    id.Account.ToString(),
                    id.Space.ToString(),
                };
            }
            else
            {
                paths = new string[] 
                {
                    "Entries",
                    id.Storage.ToString(),
                    id.Account.ToString(),
                    id.Space.ToString(),
                    id.Era.ToString(),
                    id.Period.ToString(),
                    id.Moment.ToString()
                };
            }

            return ContainerIdentifier.FromPaths(paths);
        }

        public static ContainerIdentifier FromIds(Guid storageId, Guid accountId, Guid spaceId)
        {
            var paths = new string[] 
            {
                "Entries",
                storageId.ToString(),
                accountId.ToString(),
                spaceId.ToString()
            };
            return ContainerIdentifier.FromPaths(paths);
        }

        internal static Identifier ToIdentifier(Guid storageId, Guid accountId, Guid spaceId, ContainerIdentifier containerId)
        {
            var paths = containerId.Paths;
            var era = Convert.ToUInt64(paths[4]);
            var period = Convert.ToUInt64(paths[5]);
            var moment = Convert.ToUInt64(paths[6]);
            return Identifier.Create(storageId, accountId, spaceId, era, period, moment);
        }
    }
}