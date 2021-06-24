// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Linq;
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

            // No extensions on the Storage connection (yet).
            //foreach (var extension in configuration.Extensions)
            //[
            //    extension.Initialize(container)
            //]
            var connection = container.GetInstance<IStorageConnection>();
            return connection;
        }
    }
}
