namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.MicroContainer;

    public class HostConfiguration : IHostConfiguration
    {
        public IHostExtension[] Extensions => _extensions;
        private IHostExtension[] _extensions;

        public IStorage Storage => _storage;
        private IStorage _storage;

        public IInfrastructure Infrastructure => _infrastructure;
        private IInfrastructure _infrastructure;

        private Func<Container, IHost> _getHost; 
        public IHost GetHost(Container container)
        {
            return _getHost(container);
        }


        public HostConfiguration()
        {
            _extensions = new IHostExtension[0];

            _getHost = container =>
            {
                container.Register<IHost, DefaultHost>();
                return container.GetInstance<IHost>();
            };
        }

        public IHostConfiguration Use(IHostExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IHostConfiguration Use(IStorage storage)
        {
            if (storage == null)
            {
                throw new ArgumentException(nameof(storage));
            }

            _storage = storage;

            return this;
        }
        public IHostConfiguration Use(IInfrastructure infrastructure)
        {
            if (infrastructure == null)
            {
                throw new ArgumentException(nameof(infrastructure));
            }

            _infrastructure = infrastructure;

            return this;
        }

        public IHostConfiguration Use<THost>()
            where THost : class, IHost
        {
            //if (_getHost != null)
            //{
            //    throw new InvalidOperationException("GetHost already set.");
            //}

            _getHost = container =>
            {
                container.Register<IHost, THost>();
                return container.GetInstance<IHost>();
            };

            return this;
        }
    }
}
