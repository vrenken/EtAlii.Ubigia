namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

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
            var managementConnection = await systemConnection.OpenManagementConnection();

            // Add the system user.
            await managementConnection.Accounts.Add(systemAccountName, systemAccountPassword, AccountTemplate.System);

            // Add the system user.
            await managementConnection.Accounts.Add(administratorAccountName, administratorAccountPassword, AccountTemplate.Administrator);

            await managementConnection.Close();
        }
    }
}