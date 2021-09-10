// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public class SpaceConnectionFactory
    {
        public ISpaceConnection Create(ISpaceConnectionOptions options)
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
                new SpaceConnectionScaffolding(options),
            })
            .ToArray();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            // No extensions on the Space connection (yet).
            //foreach (var extension in options.Extensions)
            //[
            //    extension.Initialize(container)
            //]
            var connection = container.GetInstance<ISpaceConnection>();
            return connection;
        }
    }
}
