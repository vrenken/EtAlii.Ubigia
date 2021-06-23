// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public class SpaceConnectionFactory : ISpaceConnectionFactory
    {
        public ISpaceConnection Create(ISpaceConnectionConfiguration configuration)
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
                new SpaceConnectionScaffolding(configuration),
            })
            .ToArray();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            // No extensions on the Space connection (yet).
            //foreach (var extension in configuration.Extensions)
            //[
            //    extension.Initialize(container)
            //]
            var connection = container.GetInstance<ISpaceConnection>();
            return connection;
        }
    }
}
