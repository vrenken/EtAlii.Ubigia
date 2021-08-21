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
            container.Register<ILogicalContext, LogicalContext>();
            container.Register<ILogicalOptions>(() => _options);
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
