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

        // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
        // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
        // specified by SonarQube. The current setup here is already some kind of facade that hides away many specific logical operations. Therefore refactoring to facades won't work.
        // Therefore this pragma warning disable of S107.
#pragma warning disable S107
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
#pragma warning restore S107
    }
}
