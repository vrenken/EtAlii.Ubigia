// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.SignalR;

    public class ContentDefinitionHub : HubBase
    {
        private readonly IContentDefinitionRepository _items;

        public ContentDefinitionHub(
            IContentDefinitionRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }

        public async Task<ContentDefinition> Get(Identifier entryId)
        {
            ContentDefinition response = null;
            try
            {
                response = await _items
                    .Get(entryId)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a ContentDefinition GET client request", e);
            }
            return response;
        }

        // Post a new ContentDefinition for the specified entry.
        public void Post(Identifier entryId, ContentDefinition contentDefinition)
        {
            try
            {
                // Store the ContentDefinition.
                _items.Store(entryId, contentDefinition);

                // Send the updated event.
                SignalUpdated(entryId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a ContentDefinition POST client request", e);
            }
        }

        // Post a new ContentDefinitionPart for the specified entry.
        public async Task PostPart(Identifier entryId, ulong contentDefinitionPartId, ContentDefinitionPart contentDefinitionPart)
        {
            try
            {
                if (contentDefinitionPartId != contentDefinitionPart.Id)
                {
                    throw new InvalidOperationException("ContentDefinitionPartId does not match");
                }

                // Store the ContentDefinition.
                await _items
                    .Store(entryId, contentDefinitionPart)
                    .ConfigureAwait(false);

                // Send the updated event.
                SignalUpdated(entryId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a ContentDefinition POST client request", e);
            }
        }


        private void SignalUpdated(in Identifier identifier)
        {
            Clients.All.SendAsync("updated", new object[] { identifier });
            //Clients.All.updated(identifier)
        }

//        private void SignalStored(Identifier identifier)
//        [
//            Clients.All.SendAsync("stored", new object[] [ identifier ])
//            //Clients.All.stored(identifier)
//        ]
    }
}
