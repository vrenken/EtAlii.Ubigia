// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly LogicalContextOptions _options;

        public ContextScaffolding(LogicalContextOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            container.Register<ILogicalContext, LogicalContext>();
            container.Register<ILogicalContextOptions>(() => _options);
            container.Register(() => _options.ConfigurationRoot);

            container.Register(() => new FabricContextFactory().Create(_options));
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
