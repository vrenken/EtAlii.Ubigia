namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    using System;
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

        public ContentDefinition Get(Identifier entryId)
        {
            ContentDefinition response = null;
            try
            {
                response = (ContentDefinition)_items.Get(entryId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a ContentDefinition GET client request", e);
            }
            return response;
        }

        // Post a new contentdefinition for the specified entry.
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
        public void PostPart(Identifier entryId, ulong contentDefinitionPartId, ContentDefinitionPart contentDefinitionPart)
        {
            try
            {
                if (contentDefinitionPartId != contentDefinitionPart.Id)
                {
                    throw new InvalidOperationException("ContentDefinitionPartId does not match");
                }

                // Store the ContentDefinition.
                _items.Store(entryId, contentDefinitionPart);

                // Send the updated event.
                SignalUpdated(entryId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a ContentDefinition POST client request", e);
            }
        }


        private void SignalUpdated(Identifier identifier)
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
