namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.SignalR;

    public class RootHub : HubBase
    {
        private readonly IRootRepository _items;

        public RootHub(
            IRootRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }

        // Get all spaces for the specified accountid
        public async Task<IEnumerable<Root>> GetForSpace(Guid spaceId) 
        {
            IEnumerable<Root> response;
            try
            {
                response = await _items
                    .GetAll(spaceId)
                    .ToArrayAsync()
                    .ConfigureAwait(false); // TODO: AsyncEnumerable
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Root GET client request", e);
            }
            return response;
        }


        // Get Item by id
        public async Task<Root> GetById(Guid spaceId, Guid rootId)
        {
            Root response;
            try
            {
                response = await _items
                    .Get(spaceId, rootId)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Root GET client request", e);
            }
            return response;
        }

        // Get Item by id
        public async Task<Root> GetByName(Guid spaceId, string rootName)
        {
            Root response;
            try
            {
                response = await _items
                    .Get(spaceId, rootName)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Root GET client request", e);
            }
            return response;
        }

        // Add item
        public async Task<Root> Post(Guid spaceId, Root root)
        {
            Root response;
            try
            {
                response = await _items
                    .Add(spaceId, root)
                    .ConfigureAwait(false);

                if (response == null)
                {
                    throw new InvalidOperationException("Unable to add root");
                }

                // Send the add event.
                SignalAdded(root.Id); // spaceId, 
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Root POST client request", e);
            }
            return response;
        }

        // Update Item by id
        public async Task<Root> Put(Guid spaceId, Guid rootId, Root root)
        {
            Root response;
            try
            {
                response = await _items
                    .Update(spaceId, rootId, root)
                    .ConfigureAwait(false);

                // Send the changed event.
                SignalChanged(root.Id); // spaceId, 
            }
            catch (Exception e)
            {
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
                SignalRemoved(rootId); // spaceId, 
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Root DELETE client request", e);
            }
        }

        ///=================================
        
        private void SignalAdded(
            //Guid spaceId, 
            Guid rootId)
        {
            Clients.All.SendAsync("added", new object[] { rootId });
            //Clients.All.added(rootId)
        }

        private void SignalChanged(
            //Guid spaceId, 
            Guid rootId)
        {
            Clients.All.SendAsync("changed", new object[]{ rootId });
            //Clients.All.changed(rootId)
        }

        private void SignalRemoved(
            //Guid spaceId, 
            Guid rootId)
        {
            Clients.All.SendAsync("removed", new object[] { rootId });
            //Clients.All.removed(rootId)
        }
    }
}
