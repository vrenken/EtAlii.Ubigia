namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class PropertiesHub : HubBase
    {
        private readonly IPropertiesRepository _items;

        public PropertiesHub(
            IPropertiesRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
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
                Clients.All.InvokeAsync("stored", new object[] { entryId });
                //Clients.All.stored(entryId);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to serve a Properties POST client request", e);
            }
        }
    }
}
