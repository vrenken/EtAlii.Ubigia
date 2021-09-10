// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
	using EtAlii.Ubigia.Infrastructure.Functional;

	public abstract partial class HostTestContextBase<TTestHost> : EtAlii.xTechnology.Hosting.HostTestContextBase<TTestHost>
        where TTestHost : class, IInfrastructureTestHostBase
    {
	    /// <summary>
        /// The details of the service current under test.
        /// </summary>
        public ServiceDetails ServiceDetails { get; protected set; }

        /// <summary>
        /// The infrastructure against which this TestContext conducts its tests.
        /// </summary>
        protected internal IInfrastructure Infrastructure { get; private set; }

        public string SystemAccountName { get; private set; }

        public string SystemAccountPassword { get; private set; }

        public string TestAccountName { get; private set; }

        public string TestAccountPassword { get; private set; }

        public string AdminAccountName { get; private set; }

        public string AdminAccountPassword { get; private set; }

        public string HostName => Infrastructure?.Options?.Name;

        protected HostTestContextBase() : base("HostSettings.json", "ClientSettings.json")
        {
        }
    }
}
