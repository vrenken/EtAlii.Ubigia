// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class SpaceConnectionScaffolding : IScaffolding
    {
        private readonly SpaceConnectionOptions _options;

        public SpaceConnectionScaffolding(SpaceConnectionOptions options)
        {
            if (options.Transport == null)
            {
                throw new InvalidOperationException("Options contains no Transport");
            }

            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options.Transport);
            container.Register<IContextCorrelator, ContextCorrelator>();
            container.Register<IAuthenticationContext, AuthenticationContext>();
            container.Register<IEntryContext, EntryContext>();
            container.Register<IRootContext, RootContext>();
            container.Register<IContentContext, ContentContext>();
            container.Register<IPropertiesContext, PropertiesContext>();

            var scaffoldings = _options.Transport
                .CreateScaffolding(_options);

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

        }
    }
}
