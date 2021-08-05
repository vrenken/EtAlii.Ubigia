// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public class HostFactory<THost>
        where THost : class, IHost
    {
        public IHost Create(IHostOptions options, bool useWrapper)
        {
            options.Use(useWrapper);

            return CreateInternal(options);
        }

        public IHost Create(IHostOptions options)
        {
            options.Use(true);

            return CreateInternal(options);
        }

        private IHost CreateInternal(IHostOptions options)
        {
            // We want to be able to recreate the whole host.
            if (options.CreateHost == null)
            {
                options.Use(() => Create(options));
            }

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new HostScaffolding<THost>(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in options.Extensions)
            {
                extension.Register(container);
            }

            return container.GetInstance<IHost>();
        }
    }
}
