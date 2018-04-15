namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.NetworkInformation;
	using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public sealed class HostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
    }

    public partial class HostTestContext<TInfrastructureTestHost> : IHostTestContext<TInfrastructureTestHost>
        where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
	}
}
