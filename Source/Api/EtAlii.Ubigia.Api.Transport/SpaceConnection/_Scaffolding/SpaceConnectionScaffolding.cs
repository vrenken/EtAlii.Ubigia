// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class SpaceConnectionScaffolding : IScaffolding
    {
        private readonly ISpaceConnectionOptions _options;

        public SpaceConnectionScaffolding(ISpaceConnectionOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register(() => _options);
            container.Register(() => _options.Transport);

            container.Register<IContextCorrelator, ContextCorrelator>();
            container.Register<IAuthenticationContext, AuthenticationContext>();
            container.Register<IEntryContext, EntryContext>();
            container.Register<IRootContext, RootContext>();
            container.Register<IContentContext, ContentContext>();
            container.Register<IPropertiesContext, PropertiesContext>();
        }
    }
}
