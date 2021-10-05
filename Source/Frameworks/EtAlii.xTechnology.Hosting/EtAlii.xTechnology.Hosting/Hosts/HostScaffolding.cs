// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public class HostScaffolding : IScaffolding
    {
        private readonly HostOptions _options;

        public HostScaffolding(HostOptions options)
        {
            // We want to be able to recreate the whole host.
            if (options.CreateHost == null)
            {
                options.Use(() => Factory.Create<IHost>(options));
            }

            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options.ConfigurationRoot);

            if (_options.AddHostWrapper)
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
