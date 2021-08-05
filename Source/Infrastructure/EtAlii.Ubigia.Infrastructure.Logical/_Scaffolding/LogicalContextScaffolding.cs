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
        public void Register(Container container)
        {
            container.Register<ILogicalContext, LogicalContext>();
            container.RegisterInitializer<ILogicalContext>(context =>
            {
                context.Initialize(
                    container.GetInstance<ILogicalStorageSet>(),
                    container.GetInstance<ILogicalSpaceSet>(),
                    container.GetInstance<ILogicalAccountSet>(),
                    container.GetInstance<ILogicalRootSet>(),
                    container.GetInstance<ILogicalEntrySet>(),
                    container.GetInstance<ILogicalContentSet>(),
                    container.GetInstance<ILogicalContentDefinitionSet>(),
                    container.GetInstance<ILogicalPropertiesSet>(),
                    container.GetInstance<ILogicalIdentifierSet>());
            });

            container.Register(() => _options);
            container.Register(() => _options.Fabric);

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
