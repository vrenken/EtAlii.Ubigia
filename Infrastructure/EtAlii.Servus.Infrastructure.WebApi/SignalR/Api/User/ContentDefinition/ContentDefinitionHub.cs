namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using EtAlii.Servus.Api;

    public class ContentDefinitionHub : HubBase
    {
        private readonly IContentDefinitionRepository _items;

        public ContentDefinitionHub(
            IContentDefinitionRepository items,
            ISignalRAuthenticationTokenVerifier authenticationTokenVerifier)
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
                //_logger.Critical("Unable to serve a ContentDefinition GET client request", ex);
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
                //_logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                throw new InvalidOperationException("Unable to serve a ContentDefinition POST client request", e);
            }
        }

        // Post a new ContentDefinitionPart for the specified entry.
        public void Post(Identifier entryId, UInt64 contentDefinitionPartId, ContentDefinitionPart contentDefinitionPart)
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
                //_logger.Critical("Unable to serve a ContentDefinition POST client request", ex);
                    throw new InvalidOperationException("Unable to serve a ContentDefinition POST client request", e);
            }
        }


        private void SignalUpdated(Identifier identifier)
        {
            Clients.All.updated(identifier);
        }

        private void SignalStored(Identifier identifier)
        {
            Clients.All.stored(identifier);
        }
    }
}
