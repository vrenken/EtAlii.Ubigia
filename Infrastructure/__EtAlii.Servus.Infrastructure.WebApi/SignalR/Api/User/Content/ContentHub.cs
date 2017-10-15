namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using EtAlii.Servus.Api;

    public class ContentHub : HubBase
    {
        private readonly IContentRepository _items;

        public ContentHub(
            IContentRepository items,
            ISignalRAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }

        public Content Get(Identifier entryId)
        {
            Content response = null;
            try
            {
                response = (Content)_items.Get(entryId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a content GET client request", ex);
                throw new InvalidOperationException("Unable to serve a content GET client request", e);
            }
            return response;
        }

        public ContentPart Get(Identifier entryId, UInt64 contentPartId)
        {
            ContentPart response = null;
            try
            {
                response = (ContentPart)_items.Get(entryId, contentPartId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Content part GET client request", ex);
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
        public void Post(Identifier entryId, Content content)
        {
            try
            {
                // Store the content.
                _items.Store(entryId, content);

                // Send the updated event.
                SignalUpdated(entryId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Content POST client request", ex);
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
        public void Post(Identifier entryId, UInt64 contentPartId, ContentPart contentPart)
        {
            try
            {
                if (contentPartId != contentPart.Id)
                {
                    throw new InvalidOperationException("ContentPartId does not match");
                }


                // Store the content.
                _items.Store(entryId, contentPart);

                // Send the updated event.
                SignalUpdated(entryId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Content part POST client request", ex);
                throw new InvalidOperationException("Unable to serve a Content part POST client request", e);
            }
        }

        private void SignalUpdated(Identifier identifier)
        {
            Clients.All.updated(identifier);
        }
    }
}
