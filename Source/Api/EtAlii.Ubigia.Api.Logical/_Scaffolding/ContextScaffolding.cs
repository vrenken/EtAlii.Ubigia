// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly LogicalContextConfiguration _configuration;

        public ContextScaffolding(LogicalContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<ILogicalContext, LogicalContext>();
            container.Register<ILogicalContextConfiguration>(() => _configuration);

            container.Register(() => new FabricContextFactory().Create(_configuration));
            container.Register<ILogicalNodeSet, LogicalNodeSet>();
            container.Register<ILogicalRootSet, LogicalRootSet>();

            container.Register<IPropertiesManager, PropertiesManager>();
            container.Register<IPropertiesGetter, PropertiesGetter>();

            container.Register(() =>
            {
                var fabric = container.GetInstance<IFabricContext>();
                return new ContentManagerFactory().Create(fabric);
            });
        }
    }
}