using SimpleInjector;
using System;

namespace EtAlii.Servus.Infrastructure.Model
{
    public class InitializerBase_ToRemove
    {
        protected Container Container { get { return _container; } }
        private readonly Container _container;

        protected IStorageRepository StorageRepository { get { return _storageRepository.Value; } }
        private readonly Lazy<IStorageRepository> _storageRepository;

        protected IEntryRepository EntryRepository { get { return _entryRepository.Value; } }
        private readonly Lazy<IEntryRepository> _entryRepository;

        protected IIdentifierRepository IdentifierRepository { get { return _identifierRepository.Value; } }
        private readonly Lazy<IIdentifierRepository> _identifierRepository;

        protected ISpaceRepository SpaceRepository { get { return _spaceRepository.Value; } }
        private readonly Lazy<ISpaceRepository> _spaceRepository;

        protected IRootRepository RootRepository { get { return _rootRepository.Value; } }
        private readonly Lazy<IRootRepository> _rootRepository;

        public InitializerBase_ToRemove(Container container)
        {
            _container = container;
            _storageRepository = new Lazy<IStorageRepository>(() => Container.GetInstance<IStorageRepository>());
            _spaceRepository = new Lazy<ISpaceRepository>(() => Container.GetInstance<ISpaceRepository>());
            _rootRepository = new Lazy<IRootRepository>(() => Container.GetInstance<IRootRepository>());
            _identifierRepository = new Lazy<IIdentifierRepository>(() => Container.GetInstance<IIdentifierRepository>());
            _entryRepository = new Lazy<IEntryRepository>(() => Container.GetInstance<IEntryRepository>());
        }
    }
}