// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    internal class LocalStorageInitializer : ILocalStorageInitializer
    {
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

        private readonly IConfigurationSection _configuration;

        public LocalStorageInitializer(IConfigurationRoot configurationRoot, ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            _systemConnectionCreationProxy = systemConnectionCreationProxy;

            _configuration = configurationRoot.GetSection("Infrastructure:Functional:Setup");
        }

        public async Task Initialize(Storage localStorage)
        {
            var systemAccountName = _configuration.GetValue<string>("DefaultSystemAccountName");
            var systemAccountPassword = _configuration.GetValue<string>("DefaultSystemAccountPassword");

            var administratorAccountName = _configuration.GetValue<string>("DefaultAdministratorAccountName");
            var administratorAccountPassword = _configuration.GetValue<string>("DefaultAdministratorAccountPassword");

            // Create a system connection.
            var systemConnection = _systemConnectionCreationProxy.Request();
            var managementConnection = await systemConnection.OpenManagementConnection().ConfigureAwait(false);

            // Add the system user.
            await managementConnection.Accounts.Add(systemAccountName, systemAccountPassword, AccountTemplate.System).ConfigureAwait(false);

            // Add the system user.
            await managementConnection.Accounts.Add(administratorAccountName, administratorAccountPassword, AccountTemplate.Administrator).ConfigureAwait(false);

            await managementConnection.Close().ConfigureAwait(false);
        }
    }
}
