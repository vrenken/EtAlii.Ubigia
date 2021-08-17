// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly IFabricContextOptions _options;

        public ContextScaffolding(IFabricContextOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
#if DEBUG
            if (_options.Connection == null)
            {
                throw new InvalidOperationException("No Connection provided");
            }
#endif
            container.Register<IFabricContext, FabricContext>();
            container.Register(() => _options);
            container.Register(() => _options.ConfigurationRoot);
            container.Register(() => _options.Connection);
        }
    }
}
