// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
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

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);

            container.Register<IHost, THost>();
            if (_options.UseWrapper)
            {
                // During runtime, so not during testing,
                // we want to be able to restart (and therefore replace) the host.
                // Hence we need a HostWrapper.
                container.RegisterDecorator<IHost, HostWrapper>();
            }
            container.RegisterInitializer<IHost>((_, host) =>
            {
                var commands = _options.Commands
                    .Distinct()
                    .ToList();

                // add the shutdown command.
                commands.AddRange(new ICommand[]
                {
                    new StartHostCommand(host),
                    new StopHostCommand(host),
                    new ShutdownHostCommand(host),
                });

                // Activate the commands and status items.
                host.Setup(commands.ToArray());
            });
        }
    }
}
