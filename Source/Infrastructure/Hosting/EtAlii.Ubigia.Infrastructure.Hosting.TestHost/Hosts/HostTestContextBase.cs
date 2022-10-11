// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
	using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public abstract partial class HostTestContextBase : HostTestContextBase<InfrastructureTestHost, InfrastructureHostServicesFactory>
    {
	    /// <summary>
        /// The details of the service current under test.
        /// </summary>
        public ServiceDetails ServiceDetails { get; protected set; }

        /// <summary>
        /// The infrastructure against which this TestContext conducts its tests.
        /// </summary>
        protected internal IFunctionalContext Functional { get; private set; }

        public string SystemAccountName { get; private set; }

        public string SystemAccountPassword { get; private set; }

        public string TestAccountName { get; private set; }

        public string TestAccountPassword { get; private set; }

        public string AdminAccountName { get; private set; }

        public string AdminAccountPassword { get; private set; }

        public string HostName { get; private set; }

        protected override InfrastructureTestHost CreateTestHost(IService[] services) => new (services);

        protected HostTestContextBase() : base("HostSettings.json", "ClientSettings.json")
        {
        }
    }
}
