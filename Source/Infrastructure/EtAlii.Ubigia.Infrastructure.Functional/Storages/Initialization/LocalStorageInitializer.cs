// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;

    internal class LocalStorageInitializer : ILocalStorageInitializer
    {
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

        public LocalStorageInitializer(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            _systemConnectionCreationProxy = systemConnectionCreationProxy;
        }

        public async Task Initialize(Storage localStorage)
        {
            var systemAccountName = AccountName.System;
            var systemAccountPassword = "system123";

            var administratorAccountName = AccountName.Administrator;
            var administratorAccountPassword = "administrator123";


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