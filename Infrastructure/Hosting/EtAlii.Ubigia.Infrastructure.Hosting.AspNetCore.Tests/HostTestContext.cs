namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Net.NetworkInformation;
	using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

	public sealed class HostTestContext : HostTestContextBase
    {
	    public override void Start()
	    {
		    //var tempFolder = Path.Combine(Path.GetTempPath(), "EtAlii", "Ubigia", Guid.NewGuid().ToString());//  "%LOCALAPPDATA%\\EtAlii\\Ubigia";

		    //var ports = GetAvailableTcpPorts(64000, 3)
			   // .Select(p => p.ToString())
			   // .ToArray();

		    //var userPort = ports[0];//"64000";
		    //var adminPort = ports[1];//"64001";
		    //var authenticationPort = ports[2];//"64002";

		    var userPort = "64000";
		    var adminPort = "64001";
		    //var authenticationPort = "64002";

			var applicationConfiguration = new ConfigurationBuilder()
				.AddInMemoryCollection(new Dictionary<string, string>()
			    {
				    { "Host:Systems:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.InfrastructureSystemFactory, EtAlii.Ubigia.Infrastructure.Transport"  },

				    { "Host:Systems:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.StorageServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.InMemory"  },
				    { "Host:Systems:0:Services:0:Name", "Debug storage"  },
				    //{ "Host:Systems:0:Services:0:BaseFolder", tempFolder  },

				    { "Host:Systems:0:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.InfrastructureServiceFactory, EtAlii.Ubigia.Infrastructure.Transport" },
					{ "Host:Systems:0:Services:1:Name", "Debug storage" },
					{ "Host:Systems:0:Services:1:Address", "http://127.0.0.1:64000" },

					//{ "Host:Systems:0:Services:2:Factory", "EtAlii.Ubigia.Infrastructure.Transport.AspNetCore.AuthenticationServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.AspNetCore" },
					//{ "Host:Systems:0:Services:2:IpAddress", "0.0.0.0" },
					//{ "Host:Systems:0:Services:2:Port", authenticationPort },

				    { "Host:Systems:0:Modules:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.AspNetCore.AdminModuleFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.AspNetCore" },
				    { "Host:Systems:0:Modules:0:Name", "Admin" },
				    { "Host:Systems:0:Modules:0:IpAddress", "127.0.0.1" }, // Admin should be accessible over localhost only
				    { "Host:Systems:0:Modules:0:Port", adminPort },

				    { "Host:Systems:0:Modules:0:Modules:0:Name", "Api" },
				    { "Host:Systems:0:Modules:0:Modules:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.AspNetCore.AdminRestServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest.AspNetCore" },
					{ "Host:Systems:0:Modules:0:Modules:0:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR.AspNetCore.AdminSignalRServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.SignalR.AspNetCore" },

				    //{ "Host:Systems:0:Modules:0:Modules:1:Name", "Portal" },
				    //{ "Host:Systems:0:Modules:0:Modules:1:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore.AdminPortalControllerServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore" },
				    //{ "Host:Systems:0:Modules:0:Modules:1:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore.AdminPortalFileHostingServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.AspNetCore" },

				    { "Host:Systems:0:Modules:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.AspNetCore.UserModuleFactory, EtAlii.Ubigia.Infrastructure.Transport.User.AspNetCore" },
					{ "Host:Systems:0:Modules:1:Name", "User" },
					{ "Host:Systems:0:Modules:1:IpAddress", "0.0.0.0" },
					{ "Host:Systems:0:Modules:1:Port", userPort },

				    { "Host:Systems:0:Modules:1:Modules:0:Name", "Api" },
				    { "Host:Systems:0:Modules:1:Modules:0:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore.UserRestServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore" },
					{ "Host:Systems:0:Modules:1:Modules:0:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore.UserSignalRServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore" },

					//{ "Host:Systems:0:Modules:1:Modules:1:Name", "Portal" },
				    //{ "Host:Systems:0:Modules:1:Modules:1:Services:0:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Portal.AspNetCore.UserPortalControllerServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Portal.AspNetCore" },
				    //{ "Host:Systems:0:Modules:1:Modules:1:Services:1:Factory", "EtAlii.Ubigia.Infrastructure.Transport.User.Portal.AspNetCore.UserPortalFileHostingServiceFactory, EtAlii.Ubigia.Infrastructure.Transport.User.Portal.AspNetCore" },
				})
			    .Build();

		    var hostConfiguration = new HostConfigurationBuilder()
			    .Build(applicationConfiguration);
			    //.UseTestHost(diagnostics);

		    var host = (InfrastructureTestHost)new HostFactory<InfrastructureTestHost>().Create(hostConfiguration);

		    base.Start(host, () => host.Infrastructure);

	    }

	    private IReadOnlyList<int> GetAvailableTcpPorts(int startingPort, int numberOfPorts)
	    {
			var result = new List<int>();

		    var portArray = new List<int>();

		    var properties = IPGlobalProperties.GetIPGlobalProperties();

		    // Ignore active connections
		    var connections = properties.GetActiveTcpConnections();
		    portArray.AddRange(from n in connections
			    where n.LocalEndPoint.Port >= startingPort
			    select n.LocalEndPoint.Port);

		    // Ignore active tcp listners
		    var endPoints = properties.GetActiveTcpListeners();
		    portArray.AddRange(from n in endPoints
			    where n.Port >= startingPort
			    select n.Port);

		    // Ignore active udp listeners
		    endPoints = properties.GetActiveUdpListeners();
		    portArray.AddRange(from n in endPoints
			    where n.Port >= startingPort
			    select n.Port);

		    portArray.Sort();

		    for (var i = startingPort; i < UInt16.MaxValue; i++)
			    if (!portArray.Contains(i))
			    {
				    result.Add(i);
				    if (--numberOfPorts == 0)
				    {
						break;
				    }
				};

		    return result;
	    }

		//    public void Start()
		//    {
		//        var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.Infrastructure");
		//        //diagnostics.EnableLogging = true;

		//        // Create a Storage instance.
		//        var storageConfiguration = TestStorageConfiguration.Create()
		//            .UseInMemoryStorage();
		//        var storage = new StorageFactory().Create(storageConfiguration);

		//        // Fetch the Infrastructure configuration.
		//        var infrastructureConfiguration = TestInfrastructureConfiguration.Create();

		//        // Create fabric instance.
		//        var fabricConfiguration = new FabricContextConfiguration()
		//            .Use(storage);
		//        var fabric = new FabricContextFactory().Create(fabricConfiguration);

		//        // Create logical context instance.
		//        var logicalConfiguration = new LogicalContextConfiguration()
		//            .Use(fabric)
		//            .Use(infrastructureConfiguration.Name, infrastructureConfiguration.Address);
		//        var logicalContext = new LogicalContextFactory().Create(logicalConfiguration);

		//        // Create a Infrastructure instance.
		//        infrastructureConfiguration = infrastructureConfiguration
		//         .Use<SystemConnectionInfrastructure>()
		////.Use<TestInfrastructure>()
		////.UseAspNetCore<TestInfrastructure>()
		////.UseSignalRTestApi(diagnostics)
		////.UseWebApiAdminApi()
		////.UseWebApiAdminPortal()
		////.UseWebApiUserApi()
		////.UseWebApiUserPortal()
		////.Use(diagnostics)
		//.Use(logicalContext);
		//        var infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);

		//        // Create a host instance.
		//        var hostConfiguration = new HostConfiguration()
		//            .UseTestHost(diagnostics)
		//            //.UseInfrastructure(storage, infrastructure)
		//;
		//        var host = new HostFactory<TestHost>().Create(hostConfiguration);

		//        // Start hosting both the infrastructure and the storage.
		//        Start(host, infrastructure);
		//    }
	}
}
