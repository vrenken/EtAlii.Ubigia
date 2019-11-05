namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.Extensions.Configuration;

	public partial class HostTestContext<TInfrastructureTestHost> 
	    where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
	    protected override void StartInternal(bool useRandomPorts)
	    {
            //var tempFolder = Path.Combine(Path.GetTempPath(), "EtAlii", "Ubigia", Guid.NewGuid().ToString());//  "%LOCALAPPDATA%\\EtAlii\\Ubigia"

            var ports = GetAvailableTcpPorts(64000, 2)
             .Select(p => p.ToString())
             .ToArray();

            var userPort = useRandomPorts ? ports[0] : "64000";
            var adminPort = useRandomPorts ? ports[1] : "64001";

            var applicationConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { "Host:Systems:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.InfrastructureSystemFactory, EtAlii.Ubigia.Infrastructure.Transport"  },

                    { "Host:Systems:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.InMemory.StorageServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.InMemory"  },
                    { "Host:Systems:0:Services:0:Name", "Debug storage"  },
				    //[ "Host:Systems:0:Services:0:BaseFolder", tempFolder  ],

				    { "Host:Systems:0:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.InfrastructureServiceFactory, EtAlii.Ubigia.Infrastructure.Transport" },
                    { "Host:Systems:0:Services:1:Name", "Debug storage" },
                    { "Host:Systems:0:Services:1:Address", "http://127.0.0.1" },

					//[ "Host:Systems:0:Services:2:Factory", "EtAlii.Ubigia.Infrastructure.Transport.NetCore.AuthenticationServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.NetCore" ],
					//[ "Host:Systems:0:Services:2:IpAddress", "0.0.0.0" ],
					//[ "Host:Systems:0:Services:2:Port", authenticationPort ],

				    { "Host:Systems:0:Modules:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.NetCore.AdminModuleFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.NetCore" },
                    { "Host:Systems:0:Modules:0:Name", "Admin" },
                    { "Host:Systems:0:Modules:0:IpAddress", "127.0.0.1" }, // Admin should be accessible over localhost only
				    { "Host:Systems:0:Modules:0:Port", adminPort },

                    { "Host:Systems:0:Modules:0:Modules:0:Name", "Api" },
                    { "Host:Systems:0:Modules:0:Modules:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.AdminRestServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest" },
                    { "Host:Systems:0:Modules:0:Modules:0:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR.AdminSignalRServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR" },

				    //[ "Host:Systems:0:Modules:0:Modules:1:Name", "Portal" ],
				    //[ "Host:Systems:0:Modules:0:Modules:1:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor.AdminPortalControllerServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor" ],
				    //[ "Host:Systems:0:Modules:0:Modules:1:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor.AdminPortalFileHostingServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor" ],

				    { "Host:Systems:0:Modules:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.NetCore.UserModuleFactory, EtAlii.Ubigia.Infrastructure.Transport.User.NetCore" },
                    { "Host:Systems:0:Modules:1:Name", "User" },
                    { "Host:Systems:0:Modules:1:IpAddress", "0.0.0.0" },
                    { "Host:Systems:0:Modules:1:Port", userPort },

                    { "Host:Systems:0:Modules:1:Modules:0:Name", "Api" },
                    { "Host:Systems:0:Modules:1:Modules:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.UserRestServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest" },
                    { "Host:Systems:0:Modules:1:Modules:0:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.UserSignalRServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR" },

					//[ "Host:Systems:0:Modules:1:Modules:1:Name", "Portal" ],
				    //[ "Host:Systems:0:Modules:1:Modules:1:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor.UserPortalControllerServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor" ],
				    //[ "Host:Systems:0:Modules:1:Modules:1:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor.UserPortalFileHostingServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Razor" ],
				})
                .Build();

            var hostConfiguration = new HostConfigurationBuilder()
                .Build(applicationConfiguration);
            //.UseTestHost(diagnostics)

            var host = new HostFactory<TInfrastructureTestHost>().Create(hostConfiguration);
            Host = host;
            host.Start();

            Infrastructure = host.Infrastructure;
        }

        protected override void StopInternal()
        {
			Host.Stop();
			Host = null;
        }
    }
}
