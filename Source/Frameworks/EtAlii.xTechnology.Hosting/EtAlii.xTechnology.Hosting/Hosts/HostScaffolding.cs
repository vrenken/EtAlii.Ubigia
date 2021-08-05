// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public class HostScaffolding<THost> : IScaffolding
        where THost : class, IHost
    {
        private readonly IHostOptions _options;

        public HostScaffolding(IHostOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);

            container.Register<IHost, THost>();
            if (_options.UseWrapper)
            {
                // During runtime, so not during testing,
                // we want to be able to restart (and therefore replace) the host.
                // Hence we need a HostWrapper.
                container.RegisterDecorator(typeof(IHost), typeof(HostWrapper));
            }
            container.RegisterInitializer<IHost>(host =>
            {
                var instanceCreator = new InstanceCreator();
                var serviceFactory = new ServiceFactory(instanceCreator);
                var moduleFactory = new ModuleFactory(serviceFactory, instanceCreator);
                var systemFactory = new SystemFactory(serviceFactory, moduleFactory, instanceCreator);

                // Instantiate the systems.
                var systems = _options.CreateSystems(host, systemFactory, serviceFactory, moduleFactory);

                // Fetch all available commands.
                var commands = _options.Commands
                    .Concat(systems.SelectMany(system => system.Commands))
                    .Distinct()
                    .ToList();

                // add the shutdown command.
                commands.AddRange(new ICommand[]
                {
                    new StartHostCommand(host),
                    new StopHostCommand(host),
                    new ShutdownHostCommand(host),
                });

                // Fetch all status items.
                var statuses = systems
                    .SelectMany(GetStatuses)
                    .Where(status => status != null)
                    .ToArray();

                // Activate the commands and status items.
                host.Setup(commands.ToArray(), statuses);

                // Initialize the services.
                var systemManager = container.GetInstance<ISystemManager>();
                systemManager.Setup(systems);

                host.Initialize();
            });
            container.Register<ISystemManager, SystemManager>();
        }

        private IEnumerable<Status> GetStatuses(ISystem system)
        {
            yield return system.Status;
            foreach (var module in system.Modules)
            {
                var statuses = GetStatuses(module);
                foreach (var status in statuses)
                {
                    yield return status;
                }
            }
            foreach (var service in system.Services)
            {
                yield return service.Status;
            }
        }

        private IEnumerable<Status> GetStatuses(IModule module)
        {
            yield return module.Status;
            foreach (var childModule in module.Modules)
            {
                var statuses = GetStatuses(childModule);
                foreach (var status in statuses)
                {
                    yield return status;
                }
            }
            foreach (var service in module.Services)
            {
                yield return service.Status;
            }
        }
    }
}
