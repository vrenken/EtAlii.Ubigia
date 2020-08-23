namespace EtAlii.Ubigia.PowerShell.Tests
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;
    using Xunit;

    public class PowerShellTestContext
    {
        private PowerShell PowerShell { get; set; }

        public IHostTestContext<InfrastructureTestHost> Context { get; private set; }

        public async Task Start()
        {
			// TODO: The PowerShell tests cannot use the in-process test infrastructure because process boundaries disable direct interaction 
			// between the host/infrastructure and the unit tests.

			Context = new HostTestContextFactory().Create<NetworkedInfrastructureHostTestContext>();
	        await Context.Start();

			PowerShell = CreatePowerShell();
			InvokeSelectStorage();
		}

		public async Task Stop()
        {
            PowerShell.Dispose();
            PowerShell = null;
            
            await Context.Stop();
            Context = null;
            PowerShellClient.Current = null;
        }

        private PowerShell CreatePowerShell()
        {
            var path = typeof(Nouns).Assembly.GetName().CodeBase!.Replace("file:///", string.Empty);
            var powerShell = PowerShell.Create(RunspaceMode.NewRunspace);
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

        public T[] ToAssertedResults<T>(Collection<PSObject> result)
            where T: class
        {
            Assert.NotNull(result);
            var typedResult = result.Select(r => r.BaseObject).Cast<T>().ToArray();
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
            PowerShell.Commands.Clear();
	        PowerShell
	            .AddCommand("Select-Storage")
				.AddArgument(Context.ServiceDetails.ManagementAddress)
                .AddArgument(Context.AdminAccountName)
                .AddArgument(Context.AdminAccountPassword);
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
