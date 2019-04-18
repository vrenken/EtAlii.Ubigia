namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNetCore.SignalR;

    public partial class EntryHub : HubBase
    {
        private readonly IEntryRepository _items;

        public EntryHub(
            IEntryRepository items,
            ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }



        private void SignalPrepared(Identifier identifier)
        {
            Clients.All.SendAsync("prepared", new object[] { identifier });
            //Clients.All.prepared(identifier)
        }

        private void SignalStored(Identifier identifier)
        {
            Clients.All.SendAsync("stored", new object[] { identifier });
            //Clients.All.stored(identifier)
        }

    }
}
