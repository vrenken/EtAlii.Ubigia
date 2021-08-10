// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LogicalContextScaffolding : IScaffolding
    {
        private readonly ILogicalContextOptions _options;

        public LogicalContextScaffolding(ILogicalContextOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<ILogicalContext, LogicalContext>();
            container.RegisterInitializer<ILogicalContext>((services, context) =>
            {
                context.Initialize(
                    services.GetInstance<ILogicalStorageSet>(),
                    services.GetInstance<ILogicalSpaceSet>(),
                    services.GetInstance<ILogicalAccountSet>(),
                    services.GetInstance<ILogicalRootSet>(),
                    services.GetInstance<ILogicalEntrySet>(),
                    services.GetInstance<ILogicalContentSet>(),
                    services.GetInstance<ILogicalContentDefinitionSet>(),
                    services.GetInstance<ILogicalPropertiesSet>(),
                    services.GetInstance<ILogicalIdentifierSet>());
            });

            container.Register(() => _options);
            container.Register(() => _options.Fabric);
            container.Register(() => _options.ConfigurationRoot);

            container.Register<ILogicalStorageSet, LogicalStorageSet>();
            container.Register<ILocalStorageGetter, LocalStorageGetter>();

            container.Register<ILogicalSpaceSet, LogicalSpaceSet>();

            container.Register<ILogicalAccountSet, LogicalAccountSet>();

            container.Register<ILogicalRootSet, LogicalRootSet>();
            container.Register<IRootInitializer, RootInitializer>();

            container.Register<ILogicalEntrySet, LogicalEntrySet>();
            container.Register<IEntryPreparer, EntryPreparer>();

            container.Register<ILogicalContentSet, LogicalContentSet>();
            container.Register<ILogicalContentDefinitionSet, LogicalContentDefinitionSet>();
            container.Register<ILogicalPropertiesSet, LogicalPropertiesSet>();
        }
    }
}
