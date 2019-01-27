namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class StorageConnectionFactory : IStorageConnectionFactory
    {
        public IStorageConnection Create(IStorageConnectionConfiguration configuration)
        {
            if (configuration.Transport == null)
            {
                throw new InvalidOperationException("Configuration contains no Transport");
            }

            var container = new Container();

            var scaffoldings = configuration.Transport
                .CreateScaffolding()
                .Concat(new IScaffolding[]
            {
                new StorageConnectionScaffolding(configuration),
            })
            .ToArray();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            var connection = container.GetInstance<IStorageConnection>();
            return connection;
        }
    }
}
