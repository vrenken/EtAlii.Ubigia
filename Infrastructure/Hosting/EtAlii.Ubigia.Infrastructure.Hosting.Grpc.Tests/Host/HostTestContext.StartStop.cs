namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
	using EtAlii.xTechnology.Hosting;
	using Microsoft.Extensions.Configuration;

    public partial class HostTestContext<TInfrastructureTestHost> 
	    where TInfrastructureTestHost : class, IInfrastructureTestHost
    {

	    protected override void StartInternal(bool useRandomPorts)
        {
            //var tempFolder = Path.Combine(Path.GetTempPath(), "EtAlii", "Ubigia", Guid.NewGuid().ToString());//  "%LOCALAPPDATA%\\EtAlii\\Ubigia";

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
				    //{ "Host:Systems:0:Services:0:BaseFolder", tempFolder  },

				    { "Host:Systems:0:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.InfrastructureServiceFactory, EtAlii.Ubigia.Infrastructure.Transport" },
                    { "Host:Systems:0:Services:1:Name", "Debug storage" },
                    { "Host:Systems:0:Services:1:Address", "http://127.0.0.1" },

					//{ "Host:Systems:0:Services:2:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Grpc.AuthenticationServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Grpc" },
					//{ "Host:Systems:0:Services:2:IpAddress", "0.0.0.0" },
					//{ "Host:Systems:0:Services:2:Port", authenticationPort },

				    { "Host:Systems:0:Modules:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc.AdminModuleFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc" },
                    { "Host:Systems:0:Modules:0:Name", "Admin" },
                    { "Host:Systems:0:Modules:0:IpAddress", "127.0.0.1" }, // Admin should be accessible over localhost only
				    { "Host:Systems:0:Modules:0:Port", adminPort },

                    { "Host:Systems:0:Modules:0:Modules:0:Name", "Api" },
                    { "Host:Systems:0:Modules:0:Modules:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc.AdminGrpcServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc" },

				    //{ "Host:Systems:0:Modules:0:Modules:1:Name", "Portal" },
				    //{ "Host:Systems:0:Modules:0:Modules:1:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc.AdminPortalControllerServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Grpc" },
				    //{ "Host:Systems:0:Modules:0:Modules:1:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc.AdminPortalFileHostingServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Grpc" },

				    { "Host:Systems:0:Modules:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Grpc.UserModuleFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Grpc" },
                    { "Host:Systems:0:Modules:1:Name", "User" },
                    { "Host:Systems:0:Modules:1:IpAddress", "0.0.0.0" },
                    { "Host:Systems:0:Modules:1:Port", userPort },

                    { "Host:Systems:0:Modules:1:Modules:0:Name", "Api" },
                    { "Host:Systems:0:Modules:1:Modules:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc.UserGrpcServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc" },

					//{ "Host:Systems:0:Modules:1:Modules:1:Name", "Portal" },
				    //{ "Host:Systems:0:Modules:1:Modules:1:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Grpc.UserPortalControllerServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Grpc" },
				    //{ "Host:Systems:0:Modules:1:Modules:1:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Grpc.UserPortalFileHostingServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Portal.Grpc" },
				})
                .Build();

            var hostConfiguration = new HostConfigurationBuilder()
                .Build(applicationConfiguration);
            //.UseTestHost(diagnostics);

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
