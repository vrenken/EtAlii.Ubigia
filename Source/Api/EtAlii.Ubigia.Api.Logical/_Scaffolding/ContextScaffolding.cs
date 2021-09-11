// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly LogicalOptions _options;

        public ContextScaffolding(LogicalOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<ILogicalContext>(serviceCollection =>
            {
                var nodes = serviceCollection.GetInstance<ILogicalNodeSet>();
                var roots = serviceCollection.GetInstance<ILogicalRootSet>();
                var content = serviceCollection.GetInstance<IContentManager>();
                var properties = serviceCollection.GetInstance<IPropertiesManager>();
                return new LogicalContext(_options, nodes, roots, content, properties);
            });
            container.Register(() => _options.ConfigurationRoot);
            container.Register(() => _options.FabricContext);
            container.Register<ILogicalNodeSet, LogicalNodeSet>();
            container.Register<ILogicalRootSet, LogicalRootSet>();

            container.Register<IPropertiesManager, PropertiesManager>();
            container.Register<IPropertiesGetter, PropertiesGetter>();

            container.Register(services =>
            {
                var fabric = services.GetInstance<IFabricContext>();
                return new ContentManagerFactory().Create(fabric);
            });
        }
    }
}
