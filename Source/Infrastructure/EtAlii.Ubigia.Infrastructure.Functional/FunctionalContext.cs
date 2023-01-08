﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Threading;

#pragma warning disable CA1724
    public class FunctionalContext : IFunctionalContext
#pragma warning restore CA1724
    {
        /// <inheritdoc />
        public IContextCorrelator ContextCorrelator { get; }

        /// <inheritdoc />
        public FunctionalContextOptions Options { get; }

        /// <inheritdoc />
        public IInformationRepository Information { get; }

        /// <inheritdoc />
        public ISpaceRepository Spaces { get; }

        /// <inheritdoc />
        public IEntryRepository Entries { get; }

        /// <inheritdoc />
        public IRootRepository Roots { get; }

        /// <inheritdoc />
        public IAccountRepository Accounts { get; }

        /// <inheritdoc />
        public IContentRepository Content { get; }

        /// <inheritdoc />
        public IContentDefinitionRepository ContentDefinition { get; }

        /// <inheritdoc />
        public IPropertiesRepository Properties { get; }

        /// <inheritdoc />
        public IStorageRepository Storages { get; }

        /// <inheritdoc />
        public ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }

        public ILogicalContext LogicalContext { get; }

        // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
        // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
        // specified by SonarQube. The current setup here is already some kind of facade that hides away many infrastructure specific variations. Therefore refactoring to facades won't work.
        // Therefore this pragma warning disable of S107.
#pragma warning disable S107
        internal FunctionalContext(
            FunctionalContextOptions options,
            IInformationRepository information,
            ISpaceRepository spaces,
            IEntryRepository entries,
            IRootRepository roots,
            IAccountRepository accounts,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IPropertiesRepository properties,
            IStorageRepository storages,
            ILogicalContext logicalContext,
            IContextCorrelator contextCorrelator,
            ISystemConnectionCreationProxy systemConnectionCreationProxy)
#pragma warning restore S107
        {
            ContextCorrelator = contextCorrelator;
            SystemConnectionCreationProxy = systemConnectionCreationProxy;
            Options = options;
            Entries = entries;
            Roots = roots;
            Content = content;
            ContentDefinition = contentDefinition;
            Properties = properties;

            Information = information;
            Spaces = spaces;
            Accounts = accounts;
            Storages = storages;
            LogicalContext = logicalContext;
        }


        /// <inheritdoc />
        public virtual async Task Start()
        {
            await LogicalContext
                .Start()
                .ConfigureAwait(false);

            await Storages
                .Initialize()
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual Task Stop()
        {
            return LogicalContext.Stop();
        }
    }
}
