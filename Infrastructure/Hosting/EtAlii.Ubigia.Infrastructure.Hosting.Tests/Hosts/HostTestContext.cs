namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public abstract partial class HostTestContextBase<TTestHost> : HostTestContext<TTestHost>
        where TTestHost : class, IInfrastructureTestHostBase
    {
        public IInfrastructure Infrastructure { get; protected set; }

        public string SystemAccountName { get; protected set; }
        public string SystemAccountPassword { get; protected set; }
        public string TestAccountName { get; protected set; }
        public string TestAccountPassword { get; protected set; }
 
        public string AdminAccountName { get; protected set; }
        public string AdminAccountPassword { get; protected set; }
        
        public string HostName => Infrastructure?.Configuration?.Name;

        protected HostTestContextBase(string configurationFile) : base(configurationFile)
        {
        }
    }
}