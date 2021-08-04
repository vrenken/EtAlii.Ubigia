// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly IFabricContextConfiguration _configuration;

        public ContextScaffolding(IFabricContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IFabricContext, FabricContext>();
            container.Register(() => _configuration);
            container.Register(() => _configuration.ConfigurationRoot);
            container.Register(() => _configuration.Connection);
        }
    }
}
