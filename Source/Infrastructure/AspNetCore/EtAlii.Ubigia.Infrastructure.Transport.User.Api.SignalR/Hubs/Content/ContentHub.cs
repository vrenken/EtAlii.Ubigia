// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.SignalR;

    public class ContentHub : HubBase
    {
        private readonly IContentRepository _items;

        public ContentHub(
            IContentRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }

        public async Task<Content> Get(Identifier entryId)
        {
            Content response;
            try
            {
                response = await _items
                    .Get(entryId)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a content GET client request", e);
            }
            return response;
        }

        public async Task<ContentPart> GetPart(Identifier entryId, ulong contentPartId)
        {
            ContentPart response;
            try
            {
                response = await _items
                    .Get(entryId, contentPartId)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Content part GET client request", e);
            }
            return response;
        }

        /// <summary>
        /// Post a new contentdefinition for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task Post(Identifier entryId, Content content)
        {
            try
            {
                // Store the content.
                await _items
                    .Store(entryId, content)
                    .ConfigureAwait(false);

                // Send the updated event.
                SignalUpdated(entryId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Content POST client request", e);
            }
        }

        /// <summary>
        /// Post a new contentPart for the specified content.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="contentPartId"></param>
        /// <param name="contentPart"></param>
        /// <returns></returns>
        public async Task PostPart(Identifier entryId, ulong contentPartId, ContentPart contentPart)
        {
            try
            {
                if (contentPartId != contentPart.Id)
                {
                    throw new InvalidOperationException("ContentPartId does not match");
                }


                // Store the content.
                await _items
                    .Store(entryId, contentPart)
                    .ConfigureAwait(false);

                // Send the updated event.
                SignalUpdated(entryId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Content part POST client request", e);
            }
        }

        private void SignalUpdated(in Identifier identifier)
        {
            Clients.All.SendAsync("updated", new object[] { identifier });
            //Clients.All.updated(identifier)
        }
    }
}
