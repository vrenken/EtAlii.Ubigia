// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class HostFactory<THost>
        where THost : class, IHost
    {
        public IHost Create(IHostConfiguration configuration, bool useWrapper)
        {
            configuration.Use(useWrapper);

            return CreateInternal(configuration);
        }

        public IHost Create(IHostConfiguration configuration)
        {
            configuration.Use(true);

            return CreateInternal(configuration);
        }

        private IHost CreateInternal(IHostConfiguration configuration)
        {
            // We want to be able to recreate the whole host.
            if (configuration.CreateHost == null)
            {
                configuration.Use(() => Create(configuration));
            }
            
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new HostScaffolding<THost>(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Register(container);
            }

            return container.GetInstance<IHost>();
        }
    }
}
