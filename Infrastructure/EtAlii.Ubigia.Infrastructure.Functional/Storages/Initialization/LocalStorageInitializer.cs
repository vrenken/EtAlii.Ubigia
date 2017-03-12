namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;

    internal class LocalStorageInitializer : ILocalStorageInitializer
    {
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

        public LocalStorageInitializer(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            _systemConnectionCreationProxy = systemConnectionCreationProxy;
        }

        public void Initialize(Storage localStorage)
        {
            var systemAccountName = AccountName.System;
            var systemAccountPassword = "system123";

            var administratorAccountName = AccountName.Administrator;
            var administratorAccountPassword = "administrator123";


            var task = Task.Run(async () =>
            {
                // Create a system connection.
                var systemConnection = _systemConnectionCreationProxy.Request();
                var managementConnection = await systemConnection.OpenManagementConnection();

                // Add the system user.
                var systemAccount = await managementConnection.Accounts.Add(systemAccountName, systemAccountPassword, AccountTemplate.System);

                // Add the system user.
                var administratorAccount = await managementConnection.Accounts.Add(administratorAccountName, administratorAccountPassword, AccountTemplate.Administrator);

                await managementConnection.Close();
            });
            task.Wait();
        }
    }
}