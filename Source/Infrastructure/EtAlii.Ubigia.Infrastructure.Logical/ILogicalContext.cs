// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System.Threading.Tasks;

    public interface ILogicalContext
    {
        ILogicalStorageSet Storages { get; }
        ILogicalSpaceSet Spaces { get; }
        ILogicalAccountSet Accounts { get; }

        ILogicalRootSet Roots { get; }
        ILogicalEntrySet Entries { get; }

        ILogicalContentSet Content { get; }
        ILogicalContentDefinitionSet ContentDefinition { get; }

        ILogicalPropertiesSet Properties { get; }
        ILogicalIdentifierSet Identifiers { get; }

        Task Start();
        Task Stop();

        void Initialize(
            ILogicalStorageSet storages,
            ILogicalSpaceSet spaces,
            ILogicalAccountSet accounts,
            ILogicalRootSet roots,
            ILogicalEntrySet entries,
            ILogicalContentSet content,
            ILogicalContentDefinitionSet contentDefinition,
            ILogicalPropertiesSet properties,
            ILogicalIdentifierSet identifiers);
    }
}