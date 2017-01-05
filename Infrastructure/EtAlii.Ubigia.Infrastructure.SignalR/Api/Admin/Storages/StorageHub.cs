namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;

    public partial class StorageHub : Hub
    {
        private readonly IStorageRepository _items;

        public StorageHub(IStorageRepository items)
        {
            _items = items;
        }

        public Storage GetLocal(string local)
        {
            Storage response;
            try
            {
                response = _items.GetLocal();
            }
            catch (Exception e)
            {
                //Logger.Critical("Unable to serve a Storage GET client request", ex);
                throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
            }
            return response;
        }

        public Storage GetByName(string storageName)
        {
            Storage response;
            try
            {
                response = _items.Get(storageName);
            }
            catch (Exception e)
            {
                //Logger.Critical("Unable to serve a Space GET client request", ex);
                throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
            }
            return response;
        }


        // Get all Items
        public IEnumerable<Storage> GetAll()
        {
            IEnumerable<Storage> response;
            try
            {
                response = _items.GetAll();
            }
            catch (Exception e)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
            }
            return response;
        }

        // Get Item by id
        public Storage Get(Guid storageId)
        {
            Storage response;
            try
            {
                response = _items.Get(storageId);
            }
            catch (Exception e)
            {
                //Logger.Warning("Unable to serve a {0} GET client request", ex, typeof(T).Name);
                throw new InvalidOperationException("Unable to serve a Storage GET client request", e);
            }
            return response;
        }

        // Add item
        public Storage Post(Storage item)
        {
            Storage response;
            try
            {
                response = _items.Add(item);
            }
            catch (Exception e)
            {
                //Logger.Warning("Unable to serve a {0} POST client request", ex, typeof(T).Name);
                throw new InvalidOperationException("Unable to serve a Storage POST client request", e);
            }
            return response;
        }

        // Update Item by id
        public Storage Put(Guid storageId, Storage storage)
        {
            Storage response;
            try
            {
                response = _items.Update(storageId, storage);
            }
            catch (Exception e)
            {
                //Logger.Warning("Unable to serve a {0} PUT client request", ex, typeof(T).Name);
                throw new InvalidOperationException("Unable to serve a Storage PUT client request", e);
            }
            return response;
        }

        // Delete Item by id
        public void Delete(Guid storageId)
        {
            try
            {
                _items.Remove(storageId);
            }
            catch (Exception e)
            {
                //Logger.Warning("Unable to serve a {0} DELETE client request", ex, typeof(T).Name);
                throw new InvalidOperationException("Unable to serve a Storage DELETE client request", e);
            }
        }
    }
}
