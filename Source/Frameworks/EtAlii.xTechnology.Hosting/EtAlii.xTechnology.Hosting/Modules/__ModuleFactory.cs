// // Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia
//
// namespace EtAlii.xTechnology.Hosting
// {
//     using System.Linq;
//     using Microsoft.Extensions.Configuration;
//
//     public class ModuleFactory
//     {
//         private readonly ServiceFactory _serviceFactory;
//         private readonly IInstanceCreator _instanceCreator;
//
//         public ModuleFactory(ServiceFactory serviceFactory, IInstanceCreator instanceCreator)
//         {
//             _serviceFactory = serviceFactory;
//             _instanceCreator = instanceCreator;
//         }
//
//         public IModule Create(
//             IHost host,
//             ISystem system,
//             IModule parentModule,
//             IConfigurationSection moduleConfiguration,
//             IConfigurationRoot configurationRoot,
//             IConfigurationDetails configurationDetails)
//         {
//             if(!_instanceCreator.TryCreate<IModule>(moduleConfiguration, configurationRoot, configurationDetails, "module", out var module))
//             {
//                 module = new DefaultModule(moduleConfiguration);
//             }
//
//             var services = moduleConfiguration
//                 .GetAllSections("Services")
//                 .Select(scs => _serviceFactory.Create(host, system, module, scs, configurationRoot, configurationDetails))
//                 .ToArray();
//
//             var modules = moduleConfiguration
//                 .GetAllSections("Modules")
//                 .Select(mcs => Create(host, system, module, mcs, configurationRoot, configurationDetails))
//                 .ToArray();
//
//             module.Setup(host, system, services, modules, parentModule);
//             return module;
//         }
//     }
// }
