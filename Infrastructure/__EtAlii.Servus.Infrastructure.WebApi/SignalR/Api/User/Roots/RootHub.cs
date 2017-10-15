﻿namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public class RootHub : HubBase
    {
        private readonly IRootRepository _items;

        public RootHub(
            IRootRepository items, 
            ISignalRAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }

        // Get all spaces for the specified accountid
        public IEnumerable<Root> GetForSpace(Guid spaceId)
        {
            IEnumerable<Root> response;
            try
            {
                response = _items.GetAll(spaceId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                throw new InvalidOperationException("Unable to serve a Root GET client request", e);
            }
            return response;
        }


        // Get Item by id
        public Root GetById(Guid spaceId, Guid rootId)
        {
            Root response;
            try
            {
                response = _items.Get(spaceId, rootId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                throw new InvalidOperationException("Unable to serve a Root GET client request", e);
            }
            return response;
        }

        // Get Item by id
        public Root GetByName(Guid spaceId, string rootName)
        {
            Root response;
            try
            {
                response = _items.Get(spaceId, rootName);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Root GET client request", ex);
                throw new InvalidOperationException("Unable to serve a Root GET client request", e);
            }
            return response;
        }

        // Add item
        public Root Post(Guid spaceId, Root root)
        {
            Root response;
            try
            {
                response = _items.Add(spaceId, root);

                if (response == null)
                {
                    throw new InvalidOperationException("Unable to add root");
                }

                // Send the add event.
                SignalAdded(spaceId, root.Id);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Root POST client request", ex);
                throw new InvalidOperationException("Unable to serve a Root POST client request", e);
            }
            return response;
        }

        // Update Item by id
        public Root Put(Guid spaceId, Guid rootId, Root root)
        {
            Root response;
            try
            {
                response = _items.Update(spaceId, rootId, root);

                // Send the changed event.
                SignalChanged(spaceId, root.Id);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Root PUT client request", ex);
                throw new InvalidOperationException("Unable to serve a Root PUT client request", e);
            }
            return response;
        }

        // Delete Item by id
        public void Delete(Guid spaceId, Guid rootId)
        {
            try
            {
                _items.Remove(spaceId, rootId);

                // Send the changed event.
                SignalRemoved(spaceId, rootId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Root DELETE client request", ex);
                throw new InvalidOperationException("Unable to serve a Root DELETE client request", e);
            }
        }

        ///=================================
        
        private void SignalAdded(Guid spaceId, Guid rootId)
        {
            Clients.All.added(rootId);
            //Hub.Clients.Group(spaceId.ToString()).added(rootId);
        }

        private void SignalChanged(Guid spaceId, Guid rootId)
        {
            Clients.All.changed(rootId);
            //Hub.Clients.Group(spaceId.ToString()).changed(rootId);
        }

        private void SignalRemoved(Guid spaceId, Guid rootId)
        {
            Clients.All.removed(rootId);
            //Hub.Clients.Group(spaceId.ToString()).removed(rootId);
        }
    }
}
