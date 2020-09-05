namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
	using System;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Networking;

	public abstract partial class HostTestContextBase<TTestHost> : HostTestContext<TTestHost>
        where TTestHost : class, IInfrastructureTestHostBase
    {
	    private readonly Guid _uniqueId = Guid.Parse("827F11D6-4305-47C6-B42B-1271052FAC86");

	    /// <summary>
        /// The details of the service current under test. 
        /// </summary>
        public ServiceDetails ServiceDetails { get; protected set; }

        /// <summary>
        /// The infrastructure against which this TestContext conducts its tests.
        /// </summary>
        protected IInfrastructure Infrastructure { get; private set; }
        public string SystemAccountName { get; private set; }
        public string SystemAccountPassword { get; private set; }
        public string TestAccountName { get; private set; }
        public string TestAccountPassword { get; private set; }
 
        public string AdminAccountName { get; private set; }
        public string AdminAccountPassword { get; private set; }
        
        public string HostName => Infrastructure?.Configuration?.Name;

        protected HostTestContextBase(string configurationFile) : base(configurationFile)
        {
        }

        protected override void StartExclusive()
	    {
		    // We want to start only one test hosting at the same time.
		    using (var _ = new SystemSafeExecutionScope(_uniqueId))
		    {
			    var task = Task.Run(async () => await StartInternal());
			    task.Wait();
		    }
	    }
	    
        protected override async Task<ConfigurationDetails> ParseForTesting(string configurationFile)
        {
	        try
	        {
		        return await new ConfigurationDetailsParser().ParseForTestingWithFreePortFindingChanges(configurationFile);
	        }
	        catch (Exception e)
	        {
		        throw new InvalidOperationException("Something fishy happened during test preparation.", e);
	        }
        }
    }
}