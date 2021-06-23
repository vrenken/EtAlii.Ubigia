// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class SpaceConnectionScaffolding : IScaffolding
    {
        private readonly ISpaceConnectionConfiguration _configuration;

        public SpaceConnectionScaffolding(ISpaceConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration);
            container.Register(() => _configuration.Transport);

            container.Register<IContextCorrelator, ContextCorrelator>();
            container.Register<IAuthenticationContext, AuthenticationContext>();
            container.Register<IEntryContext, EntryContext>();
            container.Register<IRootContext, RootContext>();
            container.Register<IContentContext, ContentContext>();
            container.Register<IPropertiesContext, PropertiesContext>();
        }
    }
}
