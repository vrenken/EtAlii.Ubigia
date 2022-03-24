// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public sealed class StorageConnectionFactory : IStorageConnectionFactory
    {
        public IStorageConnection Create(IStorageConnectionOptions options)
        {
            if (options.Transport == null)
            {
                throw new InvalidOperationException("Options contains no Transport");
            }

            var container = new Container();

            var scaffoldings = options.Transport
                .CreateScaffolding()
                .Concat(new IScaffolding[]
            {
                new StorageConnectionScaffolding(options),
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
