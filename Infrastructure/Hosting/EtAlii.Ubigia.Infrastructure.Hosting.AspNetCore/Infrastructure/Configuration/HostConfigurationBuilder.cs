//using Microsoft.Extensions.Configuration;

//namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
//{
//    using EtAlii.xTechnology.Diagnostics;
//    using EtAlii.xTechnology.Hosting.AspNetCore;
//    using System;

//    public class HostConfigurationBuilder 
//    {
//        public IHostConfiguration Build(
//            IConfigurationRoot configuration,
//            IApplicationManager applicationManager = null)
//        {
//            //var name = "EtAlii";
//            //var category = "EtAlii.Ubigia.Infrastructure";
//            ////var diagnostics = new DiagnosticsFactory().Create(true, false, true,
//            ////    () => new LogFactory(),
//            ////    () => new ProfilerFactory(),
//            ////    (factory) => factory.Create(name, category),
//            ////    (factory) => factory.Create(name, category));
//            //var diagnostics = new DiagnosticsFactory().CreateDisabled(name, category);


//            //// Create a storage instance.
//            //var storageConfigurationSection = (IStorageConfigurationSection)getConfigurationSection("ubigia/storage");
//            //var storageConfiguration = storageConfigurationSection
//            //    .ToStorageConfiguration();
//            //var storage = new StorageFactory().Create(storageConfiguration);

//            //// Fetch the Infrastructure configuration.
//            //var infrastructureConfigurationSection = (IInfrastructureConfigurationSection)getConfigurationSection("ubigia/infrastructure");
//            //var infrastructureConfiguration = infrastructureConfigurationSection.ToInfrastructureConfiguration();

//            //// Create fabric instance.
//            //var fabricConfiguration = new FabricContextConfiguration()
//            //    .Use(storage);
//            //var fabric = new FabricContextFactory().Create(fabricConfiguration);

//            //// Create logical context instance.
//            //var logicalConfiguration = new LogicalContextConfiguration()
//            //    .Use(fabric)
//            //    .Use(infrastructureConfiguration.Name, infrastructureConfiguration.Address);
//            //var logicalContext = new LogicalContextFactory().Create(logicalConfiguration);

//            //// Create a Infrastructure instance.
//            //infrastructureConfiguration = infrastructureConfiguration
//            //    .UseOwin(applicationManager)
//            //    .UseWebApi(diagnostics) // TODO: Web API usage should also be configured in the configuration section.
//            //    .UseWebApiAdminApi()
//            //    .UseWebApiAdminPortal()
//            //    .UseWebApiUserApi()
//            //    .UseWebApiUserPortal()
//            //    .UseSignalRApi()
//            //    .Use(logicalContext);
//            //infrastructure = new InfrastructureFactory().Create(infrastructureConfiguration);
            
//            // Create a host instance.
//            var hostConfigurationSection = (IHostConfigurationSection)getConfigurationSection("ubigia/host");
//            var hostConfiguration = hostConfigurationSection.ToHostConfiguration()
//                //.UseInfrastructure(storage, infrastructure)
//                .Use(systemCommands);

//            return hostConfiguration;
//        }
//    }
//}
