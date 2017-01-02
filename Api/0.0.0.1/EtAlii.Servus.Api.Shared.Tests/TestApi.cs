namespace EtAlii.Servus.Api.Tests
{
    public static class TestApi
    {
        //public static IManagementConnection CreateManagementConnection(IHostingConfiguration configuration, bool openOnCreation = true)
        //{
        //    var diagnostics = ApiTestHelper.CreateDiagnostics();
        //    var connection = new ManagementConnectionFactory().Create<WebApiDataTransport, NotificationTransportStub>(diagnostics);
        //    if (openOnCreation)
        //    {
        //        connection.Open(configuration.Address, configuration.Account, configuration.Password);
        //    }
        //    return connection;
        //}

        //public static IDataConnection CreateDataConnection(IHostingConfiguration hostingConfiguration, string spaceName, string accountName, string accountPassword, bool openOnCreation = true, bool useNewSpace = false)
        //{
        //    var diagnostics = ApiTestHelper.CreateDiagnostics();
        //    var connection = new DataConnectionFactory().Create<WebApiDataTransport, NotificationTransportStub>(diagnostics);
        //    if (openOnCreation)
        //    {
        //        if (useNewSpace)
        //        {
        //            spaceName = Guid.NewGuid().ToString();

        //            var managementConnection = CreateManagementConnection(hostingConfiguration);
        //            var account = managementConnection.Accounts.Get(accountName);
        //            if (account == null)
        //            {
        //                account = managementConnection.Accounts.Add(accountName, accountPassword);
        //            }
        //            managementConnection.Spaces.Add(account.Id, spaceName);
        //            //managementConnection.Close();
        //        }
        //        connection.Open(hostingConfiguration.Address, accountName, accountPassword, spaceName);
        //    }
        //    return connection;
        //}
    }
}
