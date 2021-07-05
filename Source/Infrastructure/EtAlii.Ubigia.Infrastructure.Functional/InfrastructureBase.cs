// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Threading;

    public abstract class InfrastructureBase : IInfrastructure
    {
        public IContextCorrelator ContextCorrelator { get; }

        /// <inheritdoc />
        public IInfrastructureConfiguration Configuration { get; }

        /// <inheritdoc />
        public IInformationRepository Information { get; }

        /// <inheritdoc />
        public ISpaceRepository Spaces { get; }

        /// <inheritdoc />
        public IIdentifierRepository Identifiers { get; }

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

        private readonly ILogicalContext _logicalContext;

        // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
        // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
        // specified by SonarQube. The current setup here is already some kind of facade that hides away many storage specific variations. Therefore refactoring to facades won't work.
        // Therefore this pragma warning disable of S107.
#pragma warning disable S107
        protected InfrastructureBase(
            IInfrastructureConfiguration configuration,
            IInformationRepository information,
            ISpaceRepository spaces,
            IIdentifierRepository identifiers,
            IEntryRepository entries,
            IRootRepository roots,
            IAccountRepository accounts,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IPropertiesRepository properties,
            IStorageRepository storages,
            ILogicalContext logicalContext,
            IContextCorrelator contextCorrelator)
#pragma warning restore S107
        {
            ContextCorrelator = contextCorrelator;
            Configuration = configuration;
            Identifiers = identifiers;
            Entries = entries;
            Roots = roots;
            Content = content;
            ContentDefinition = contentDefinition;
            Properties = properties;

            Information = information;
            Spaces = spaces;
            Accounts = accounts;
            Storages = storages;
            _logicalContext = logicalContext;
        }

        public virtual Task Start()
        {
            return _logicalContext.Start();
        }

        public virtual Task Stop()
        {
            return _logicalContext.Stop();
        }
    }
}
