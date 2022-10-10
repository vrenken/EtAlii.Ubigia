// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public static class SystemConnectionTestExtensions
    {
        public static async Task<IManagementConnection> OpenManagementConnection(this Task<(ISystemConnection, SystemConnectionOptions)> connectionTask)
        {
            var (systemConnection, _) = await connectionTask.ConfigureAwait(false);

            return await systemConnection
                .OpenManagementConnection()
                .ConfigureAwait(false);
        }

        public static Task<(ISystemConnection, SystemConnectionOptions)> CreateSystemConnection(this HostTestContextBase testContext)
        {
            var systemConnectionOptions = new SystemConnectionOptions(testContext.ClientConfiguration)
                .Use(testContext.Infrastructure)
                .Use(new SystemTransportProvider(testContext.Infrastructure));
            var systemConnection = Factory.Create<ISystemConnection>(systemConnectionOptions);

            return Task.FromResult((systemConnection, systemConnectionOptions));
        }

        public static async Task<(IDataConnection, DataConnectionOptions)> OpenSpace(this Task<(ISystemConnection, SystemConnectionOptions)> connectionTask, string accountName, string spaceName)
        {
            var (systemConnection, _) = await connectionTask.ConfigureAwait(false);

            return await systemConnection
                .OpenSpace(accountName, spaceName)
                .ConfigureAwait(false);
        }

        public static async Task<(ISystemConnection, SystemConnectionOptions)> AddUserAccountAndSpaces(this Task<(ISystemConnection, SystemConnectionOptions)> connectionTask, string accountName, string password, string[] spaceNames)
        {
            var (systemConnection, systemConnectionOptions) = await connectionTask.ConfigureAwait(false);

            var managementConnection = await systemConnection
                .OpenManagementConnection()
                .ConfigureAwait(false);
            var account = await managementConnection.Accounts
                .Add(accountName, password, AccountTemplate.User)
                .ConfigureAwait(false);

            foreach (var spaceName in spaceNames)
            {
                await managementConnection.Spaces
                    .Add(account.Id, spaceName, SpaceTemplate.Data)
                    .ConfigureAwait(false);
            }
            await managementConnection
                .DisposeAsync()
                .ConfigureAwait(false);

            return (systemConnection, systemConnectionOptions);
        }
    }
}
