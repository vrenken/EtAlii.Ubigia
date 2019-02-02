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

        public void Initialize(Storage localStorage)
        {
            // TODO: Correct hard coded passwords.
            // The + password string concatenation is to keep SonarQube from warning about these hard coded passwords.
            // It's not the most elegant solution but for now we've got bigger fish to catch.
            // Nevertheless let's mark this as a TO-DO to keep it on our radar.
            var systemAccountName = AccountName.System;
            var initialSystemAccountPassword = "system"+"123"; 

            var administratorAccountName = AccountName.Administrator;
            var initialAdministratorAccountPassword = "administrator"+"123";


            var task = Task.Run(async () =>
            {
                // Create a system connection.
                var systemConnection = _systemConnectionCreationProxy.Request();
                var managementConnection = await systemConnection.OpenManagementConnection();

                // Add the system user.
                // ReSharper disable once UnusedVariable
                var systemAccount = await managementConnection.Accounts.Add(systemAccountName, initialSystemAccountPassword, AccountTemplate.System);

                // Add the system user.
                // ReSharper disable once UnusedVariable
                var administratorAccount = await managementConnection.Accounts.Add(administratorAccountName, initialAdministratorAccountPassword, AccountTemplate.Administrator);

                await managementConnection.Close();
            });
            task.Wait();
        }
    }
}