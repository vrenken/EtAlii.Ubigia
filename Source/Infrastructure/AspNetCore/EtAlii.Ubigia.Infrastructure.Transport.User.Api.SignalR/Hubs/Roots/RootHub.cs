// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;

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

        /// <summary>
        /// Get all spaces for the specified space id.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async IAsyncEnumerable<Root> GetForSpace(Guid spaceId)
        {
            // The structure below might seem weird.
            // But it is not possible to combine a try-catch with the yield needed
            // enumerating an IAsyncEnumerable.
            // The only way to solve this is using the enumerator.
            var enumerator = _items
                .GetAll(spaceId)
                .GetAsyncEnumerator();
            var hasResult = true;
            while (hasResult)
            {
                Root item;
                try
                {
                    hasResult = await enumerator
                        .MoveNextAsync()
                        .ConfigureAwait(false);
                    item = hasResult ? enumerator.Current : null;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to serve a Root GET client request", e);
                }
                if (item != null)
                {
                    yield return item;
                }
            }
        }


        /// <summary>
        /// Get Root by id.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="rootId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// Get Root by name.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// Add a root.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Root PUT client request", e);
            }
            return response;
        }

        // Delete Item by id
        public async Task Delete(Guid spaceId, Guid rootId)
        {
            try
            {
                await _items.Remove(spaceId, rootId).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Root DELETE client request", e);
            }
        }
    }
}
