﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public abstract class InfrastructureBase : IInfrastructure
    {
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
            ILogicalContext logicalContext)
        {
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