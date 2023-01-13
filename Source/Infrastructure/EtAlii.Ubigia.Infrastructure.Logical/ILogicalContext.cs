// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System.Threading.Tasks;

public interface ILogicalContext
{
    /// <summary>
    /// Provides access to the Storages known to this infrastructure instance.
    /// </summary>
    ILogicalStorageSet Storages { get; }

    /// <summary>
    /// Provides access to the Spaces known to this infrastructure instance.
    /// </summary>
    ILogicalSpaceSet Spaces { get; }

    /// <summary>
    /// Provides access to the Accounts known to this infrastructure instance.
    /// </summary>
    ILogicalAccountSet Accounts { get; }

    /// <summary>
    /// Provides access to the Roots that provide access to the information in this this infrastructure instance.
    /// </summary>
    ILogicalRootSet Roots { get; }

    /// <summary>
    /// Provides access to the Entities that make up the information in this this infrastructure instance.
    /// </summary>
    ILogicalEntrySet Entries { get; }

    /// <summary>
    /// Provides access to the Content that makes up the information in this this infrastructure instance.
    /// </summary>
    ILogicalContentSet Content { get; }

    /// <summary>
    /// Provides access to the ContentDefinitions that make up the information in this this infrastructure instance.
    /// </summary>
    ILogicalContentDefinitionSet ContentDefinition { get; }

    /// <summary>
    /// Provides access to the Properties that decorate the information in this this infrastructure instance.
    /// </summary>
    ILogicalPropertiesSet Properties { get; }

    /// <summary>
    /// Provides access to Identifiers with which the information in this this infrastructure instance can be addressed.
    /// </summary>
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
