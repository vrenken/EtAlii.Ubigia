namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using System;
	using System.ComponentModel;
	using System.Threading;
	using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Api.Transport.WebApi;
	using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.Hosting;

    public abstract class HostTestContextBase : IHostTestContext
    {
        public IInfrastructure Infrastructure { get; private set; }
        public InfrastructureTestHost Host { get; private set; }
        public string SystemAccountName { get; private set; }
		public string SystemAccountPassword { get; private set; }
		public string TestAccountName { get; private set; }
		public string TestAccountPassword { get; private set; }

	    public string HostAddress => "http://127.0.0.1";//Infrastructure?.Configuration?.Address?.Replace("+", "127.0.0.1");
	    public string ManagementServiceAddress => $"{HostAddress}:{Host.AdminModule.Port}/Admin";
	    public string DataServiceAddress => $"{HostAddress}:{Host.UserModule.Port}/User";

		public string HostName => Infrastructure?.Configuration?.Name;

		public abstract void Start();

		protected void Start(InfrastructureTestHost host, Func<IInfrastructure> getInfrastructure)
		{
			Host = host;
			host.Start();

			//WaitUntilHostIsRunning(host);

			//WaitUntilModuleIsRunning(host.AdminModule);
			//WaitUntilModuleIsRunning(host.UserModule);

			Infrastructure = getInfrastructure();

			var systemAccount = Infrastructure.Accounts.Get("System");
			SystemAccountName = systemAccount.Name;
			SystemAccountPassword = systemAccount.Password;

			var adminAccount = Infrastructure.Accounts.Get("Administrator");
			TestAccountName = adminAccount.Name;
			TestAccountPassword = adminAccount.Password;
		}

		private void WaitUntilHostIsRunning(InfrastructureTestHost host)
	    {
		    var resetEvent = new ManualResetEvent(false);

		    void SignalResetEvent(object o, PropertyChangedEventArgs e)
		    {
			    switch (e.PropertyName)
			    {
				    case nameof(host.State):
					    if (host.State == State.Running)
					    {
						    resetEvent.Set();
					    }

					    break;
			    }
		    }

		    host.PropertyChanged += SignalResetEvent;

		    host.Start();

		    resetEvent.WaitOne(TimeSpan.FromSeconds(5), true);

		    host.PropertyChanged -= SignalResetEvent;
	    }

	    private void WaitUntilModuleIsRunning(IModule module)
	    {
		    if (module.State == State.Running)
		    {
			    return;
		    }

			var resetEvent = new ManualResetEvent(false);

		    void SignalResetEvent(object o, PropertyChangedEventArgs e)
		    {
			    switch (e.PropertyName)
			    {
				    case nameof(module.State):
					    if (module.State == State.Running)
					    {
						    resetEvent.Set();
					    }

					    break;
			    }
		    }

		    module.PropertyChanged += SignalResetEvent;

		    resetEvent.WaitOne(TimeSpan.FromSeconds(5), true);

		    module.PropertyChanged -= SignalResetEvent;
	    }

		public async Task<ISystemConnection> CreateSystemConnection()
        {
            var connectionConfiguration = new SystemConnectionConfiguration()
                .Use(Infrastructure)
                .Use(SystemTransportProvider.Create(Infrastructure));
            var connection = new SystemConnectionFactory().Create(connectionConfiguration);
            return await Task.FromResult(connection);
        }

	    public IInfrastructureClient CreateRestInfrastructureClient()
	    {
		    var httpClientFactory = new TestHttpClientFactory(this.Host.Server);
		    var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
		    return infrastructureClient;
	    }

		public void Stop()
        {
            Host.Stop();
            Host = null;

            Infrastructure = null;

	        SystemAccountName = null;
	        SystemAccountPassword = null;
	        TestAccountName = null;
	        TestAccountPassword = null;
        }

        public async Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames)
        {
            var managementConnection = await connection.OpenManagementConnection();
            var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);

            foreach (var spaceName in spaceNames)
            {
                await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Data);
            }
            await managementConnection.Close();
        }
    }
}
