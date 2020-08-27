namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public abstract partial class HostTestContextBase<TTestHost> : HostTestContext<TTestHost>
        where TTestHost : class, IInfrastructureTestHostBase
    {
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

        protected override async Task<ConfigurationDetails> ParseForTesting(string configurationFile)
        {
		    return await new ConfigurationDetailsParser().ParseForTestingWithFreePortFindingChanges(configurationFile);
        }
    }
}