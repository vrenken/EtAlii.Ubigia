namespace EtAlii.Ubigia.PowerShell.Tests
{
    using System;
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Hosting;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.Admin;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Api.User;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Diagnostics;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Storage.InMemory;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;
    using Xunit;

    public class PowerShellTestContext
    {
        private PowerShell PowerShell { get; set; }

        public HostTestContext<PowerShellTestHost> Context { get; private set; }

        public PowerShellTestContext()
        {
            Context = new HostTestContext<PowerShellTestHost>();
        }

        public void Start()
        {
            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Infrastructure"); // TODO: Diagnostics should be moved to each of the configuration sections.
            //diagnostics.EnableLogging = true;

            // Create a Storage instance.
            var storageConfiguration = TestStorageConfiguration.Create()
                .UseInMemoryStorage();
            var storage = new StorageFactory().Create(storageConfiguration);

            // Fetch the Infrastructure configuration.
            var infrastructureConfiguration = TestInfrastructureConfiguration.Create();

            // Create fabric instance.
            var fabricConfiguration = new FabricContextConfiguration()
                .Use(storage);
            var fabric = new FabricContextFactory().Create(fabricConfiguration);

            // Create logical context instance.
            var logicalConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(infrastructureConfiguration.Name, infrastructureConfiguration.Address);
            var logicalContext = new LogicalContextFactory().Create(logicalConfiguration);

            // Create a Infrastructure instance.
            infrastructureConfiguration = infrastructureConfiguration
                // The powershell tests cannot use the test infrastructure because process boundaries disable direct interaction 
                // between the host/infrastructure and the unit tests.
                //.UseTestInfrastructure(infrastructureDiagnostics)
                .UseWebApi(diagnostics)
                .UseWebApiAdminApi()
                .UseWebApiUserApi()
                .UseSignalRApi()
                .Use(logicalContext);
            var infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

            // Create a host instance.
            var hostConfiguration = new HostConfiguration()
                .UseTestHost(diagnostics)
                .UseInfrastructure(storage, infrastructure);
            var host = new HostFactory<PowerShellTestHost>().Create(hostConfiguration);

            // Start hosting both the infrastructure and the storage.
            Context.Start(host, infrastructure);
            PowerShell = CreatePowerShell();

            InvokeSelectStorage();
        }

        public void Stop()
        {
            PowerShell.Dispose();
            PowerShell = null;
            Context.Stop();
            Context = null;
            PowerShellClient.Current = null;
        }

        private PowerShell CreatePowerShell()
        {
            var path = typeof(Nouns).Assembly.GetName().CodeBase.Replace("file:///", String.Empty);
            var powerShell = PowerShell.Create();
            powerShell.Commands.AddCommand("Import-Module").AddParameter("Name", path);
            powerShell.Invoke();
            powerShell.Commands.Clear();
            return powerShell;
        }

        public T ToAssertedResult<T>(Collection<PSObject> result)
            where T: class
        {
            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            var typedResult = result[0].BaseObject as T;
            Assert.NotNull(typedResult);
            return typedResult;
        }

        #region Storages

        public Collection<PSObject> InvokeGetStorages()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Storages");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeUpdateStorage(Storage storage)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Update-Storage")
                      .AddArgument(storage);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetStorageByName(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Storage")
                      .AddArgument(name);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeAddStorage()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Add-Storage")
                       .AddParameter("Name", $"\"{Guid.NewGuid()}\"")
                       .AddParameter("Address", $"\"{Guid.NewGuid()}\"");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeAddStorage(string name, string address)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Add-Storage")
                       .AddArgument(name)
                       .AddArgument(address);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveStorageById(Guid storageId)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Storage")
                      .AddArgument(storageId)
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveStorageByName(string storageName)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Storage")
                      .AddArgument(storageName)
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveStorageByInstance(Storage storage)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Storage")
                      .AddArgument(storage)
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeSelectStorage()
        {
            var configuration = Context.Infrastructure.Configuration;
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Select-Storage")
                       .AddArgument(configuration.Address)
                       .AddArgument(configuration.Account)
                       .AddArgument(configuration.Password);
            return PowerShell.Invoke();
        }

        #endregion Storages

        #region Accounts

        public Collection<PSObject> InvokeGetAccounts()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Accounts");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeSelectAccountByName(string accountName)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Select-Account")
                      .AddArgument(accountName);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeUpdateAccount(Account account)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Update-Account")
                      .AddArgument(account);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetAccountByName(string accountName)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Account")
                      .AddArgument(accountName);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetAccountByInstance()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Account");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveAccountByName(string accountName)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Account")
                      .AddArgument(accountName)
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveAccountByInstance()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Account")
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeAddAccount(string accountName, string password, AccountTemplate template)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Add-Account")
                       .AddArgument(accountName)
                       .AddArgument(password)
                       .AddArgument(template.Name);
            var result = PowerShell.Invoke();
            return result;
        }

        #endregion Accounts

        #region Spaces

        public Collection<PSObject> InvokeGetSpaces()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Spaces");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeSelectSpaceByName(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Select-Space")
                      .AddArgument(name);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeUpdateSpace(Space space)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Update-Space")
                      .AddArgument(space);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetSpaceByName(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Space")
                      .AddArgument(name);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetSpaceByInstance()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Space");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveSpaceByName(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Space")
                      .AddArgument(name)
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveSpaceByInstance()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Space")
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeAddSpace(string name, SpaceTemplate template)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Add-Space")
                       .AddArgument(name)
                       .AddArgument(template.Name);
            var result = PowerShell.Invoke();
            return result;
        }

        #endregion Spaces

        #region Roots

        public Collection<PSObject> InvokeGetRoots()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Roots");
            var result = PowerShell.Invoke();
            return result;
        }


        public Collection<PSObject> InvokeSelectRootByName(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Select-Root")
                      .AddArgument(name);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeUpdateRoot(Root root)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Update-Root")
                      .AddArgument(root);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetRootByName(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Root")
                      .AddArgument(name);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetRootById(Guid id)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Root")
                      .AddArgument(id);
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeGetRootByInstance()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Get-Root");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveRootByName(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Root")
                      .AddArgument(name)
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeRemoveRootByInstance()
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Remove-Root")
                      .AddParameter("Force");
            var result = PowerShell.Invoke();
            return result;
        }

        public Collection<PSObject> InvokeAddRoot(string name)
        {
            PowerShell.Commands.Clear();
            PowerShell.AddCommand("Add-Root")
                       .AddArgument(name);
            var result = PowerShell.Invoke();
            return result;
        }

        #endregion Roots
    }
}
