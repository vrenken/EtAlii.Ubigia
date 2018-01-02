﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR.AspNetCore
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public partial class EntryHub : HubBase
    {
        private readonly IEntryRepository _items;

        public EntryHub(
            IEntryRepository items,
            ISignalRAuthenticationTokenVerifier authenticationTokenVerifier)
            : base(authenticationTokenVerifier)
        {
            _items = items;
        }



        private void SignalPrepared(Identifier identifier)
        {
            Clients.All.InvokeAsync("prepared", new object[] { identifier });
            //Clients.All.prepared(identifier);
        }

        private void SignalStored(Identifier identifier)
        {
            Clients.All.InvokeAsync("stored", new object[] { identifier });
            //Clients.All.stored(identifier);
        }

    }
}
