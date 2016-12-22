namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Functional;

    public class PropertiesHub : HubBase
    {
        private readonly IPropertiesRepository _items;

        public PropertiesHub(
            IPropertiesRepository items,
            ISignalRAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }

        public PropertyDictionary Get(Identifier entryId)
        {
            PropertyDictionary response = null;
            try
            {
                response = _items.Get(entryId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a properties GET client request", ex);
                throw new InvalidOperationException("Unable to serve a properties GET client request", e);
            }
            return response;
        }

        /// <summary>
        /// Post a new properties for the specified entry.
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public void Post(Identifier entryId, PropertyDictionary properties)
        {
            try
            {
                // Store the content.
                _items.Store(entryId, properties);

                // Send the updated event.
                Clients.All.stored(entryId);
            }
            catch (Exception e)
            {
                //_logger.Critical("Unable to serve a Properties POST client request", ex);
                throw new InvalidOperationException("Unable to serve a Properties POST client request", e);
            }
        }
    }
}
