namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public abstract partial class HostTestContextBase<TTestHost> : HostTestContext<TTestHost>
        where TTestHost : class, IInfrastructureTestHostBase
    {
        public IInfrastructure Infrastructure { get; private set; }

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
    }
}