﻿namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;
    using System;

    public abstract class Infrastructure : IInfrastructure
    {
        public IInfrastructureConfiguration Configuration { get { return _configuration; } }
        private readonly IInfrastructureConfiguration _configuration;

        public ISpaceRepository Spaces { get { return _spaces; } }
        private readonly ISpaceRepository _spaces;

        public IIdentifierRepository Identifiers { get { return _identifiers; } }
        private readonly IIdentifierRepository _identifiers;

        public IEntryRepository Entries { get { return _entries; } }
        private readonly IEntryRepository _entries;

        public IRootRepository Roots { get { return _roots; } }
        private readonly IRootRepository _roots;

        public IRootInitializer RootInitializer { get { return _rootInitializer; } }
        private readonly IRootInitializer _rootInitializer;

        public IAccountRepository Accounts { get { return _accounts; } }
        private readonly IAccountRepository _accounts;

        public IContentRepository Content { get { return _content; } }
        private readonly IContentRepository _content;

        public IContentDefinitionRepository ContentDefinition { get { return _contentDefinition; } }
        private readonly IContentDefinitionRepository _contentDefinition;

        public IStorageRepository Storages { get { return _storages; } }
        private readonly IStorageRepository _storages;

        internal IStorageSystem StorageSystem { get { return _storageSystem; } }
        private readonly IStorageSystem _storageSystem;

        public Infrastructure(
            IInfrastructureConfiguration configuration, 
            ISpaceRepository spaces, 
            IIdentifierRepository identifiers, 
            IEntryRepository entries, 
            IRootRepository roots, 
            IRootInitializer rootInitializer, 
            IAccountRepository accounts, 
            IContentRepository content, 
            IContentDefinitionRepository contentDefinition, 
            IStorageRepository storages, 
            IStorageSystem storageSystem)
        {
            _configuration = configuration;
            _spaces = spaces;
            _identifiers = identifiers;
            _entries = entries;
            _roots = roots;
            _rootInitializer = rootInitializer;
            _accounts = accounts;
            _content = content;
            _contentDefinition = contentDefinition;
            _storages = storages;
            _storageSystem = storageSystem;
        }

        public virtual void Start() { }

        public virtual void Stop() { }
    }
}
