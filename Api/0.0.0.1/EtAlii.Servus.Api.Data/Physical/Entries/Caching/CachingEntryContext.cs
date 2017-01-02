namespace EtAlii.Servus.Api.Data
{
    using System.Linq;
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public class CachingEntryContext : IEntryContext
    {
        private readonly object _lockObject = new object();

        private readonly EntryCacheChangeHandler _changeHandler;
        private readonly EntryCacheGetHandler _getHandler;
        private readonly EntryCacheGetRelatedHandler _getRelatedHandler;
        private readonly EntryCacheStoreHandler _storeHandler;
        private readonly EntryCacheContextProvider _contextProvider;

        internal CachingEntryContext(
            EntryCacheContextProvider contextProvider,
            EntryCacheChangeHandler changeHandler,
            EntryCacheGetHandler getHandler,
            EntryCacheGetRelatedHandler getRelatedHandler,
            EntryCacheStoreHandler storeHandler)
        {
            _changeHandler = changeHandler;
            _getHandler = getHandler;
            _getRelatedHandler = getRelatedHandler;
            _storeHandler = storeHandler;

            _contextProvider = contextProvider;
            _contextProvider.Context.Prepared += OnPrepared;
            _contextProvider.Context.Stored += OnStored;
        }

        public IEditableEntry Prepare()
        {
            return _contextProvider.Context.Prepare();
        }

        public IReadOnlyEntry Change(IEditableEntry entry)
        {
            lock (_lockObject)
            {
                return _changeHandler.Handle(entry);
            }
        }

        public IReadOnlyEntry Get(Root root)
        {
            lock (_lockObject)
            {
                return _getHandler.Handle(root.Identifier);
            }
        }

        public IReadOnlyEntry Get(Identifier identifier)
        {
            lock (_lockObject)
            {
                return _getHandler.Handle(identifier);
            }
        }

        public IEnumerable<IReadOnlyEntry> Get(IEnumerable<Identifier> identifiers)
        {
            lock (_lockObject)
            {
                return _getHandler.Handle(identifiers);
            }
        }

        public IEnumerable<IReadOnlyEntry> GetRelated(Identifier identifier, EntryRelation relations)
        {
            lock (_lockObject)
            {
                return _getRelatedHandler.Handle(identifier, relations);
            }
        }

        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

        private void OnPrepared(Identifier identifier)
        {
            lock (_lockObject)
            {
                Prepared(identifier);
            }
        }

        private void OnStored(Identifier identifier)
        {
            lock (_lockObject)
            {
                _storeHandler.Handle(identifier);
                Stored(identifier);
            }
        }
    }
}
