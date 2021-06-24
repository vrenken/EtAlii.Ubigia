// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContext : ILogicalContext
    {
        public ILogicalStorageSet Storages { get; private set; }

        public ILogicalSpaceSet Spaces { get; private set; }

        public ILogicalAccountSet Accounts { get; private set; }

        public ILogicalRootSet Roots { get; private set; }

        public ILogicalEntrySet Entries { get; private set; }

        public ILogicalContentSet Content { get; private set; }

        public ILogicalContentDefinitionSet ContentDefinition { get; private set; }

        public ILogicalPropertiesSet Properties { get; private set; }

        public ILogicalIdentifierSet Identifiers { get; private set; }

        private readonly IFabricContext _fabricContext;

        public LogicalContext(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public void Initialize(
            ILogicalStorageSet storages,
            ILogicalSpaceSet spaces,
            ILogicalAccountSet accounts,
            ILogicalRootSet roots,
            ILogicalEntrySet entries,
            ILogicalContentSet content,
            ILogicalContentDefinitionSet contentDefinition,
            ILogicalPropertiesSet properties,
            ILogicalIdentifierSet identifiers)
        {
            Storages = storages;
            Spaces = spaces;
            Accounts = accounts;
            Roots = roots;
            Entries = entries;
            Content = content;
            ContentDefinition = contentDefinition;
            Properties = properties;
            Identifiers = identifiers;
        }

        public async Task Start()
        {
            _fabricContext.Start();

            await Storages.Start().ConfigureAwait(false);
            Roots.Start();
        }

        public async Task Stop()
        {
            Roots.Stop();
            await Storages.Stop().ConfigureAwait(false);

            _fabricContext.Stop();
        }
    }
}