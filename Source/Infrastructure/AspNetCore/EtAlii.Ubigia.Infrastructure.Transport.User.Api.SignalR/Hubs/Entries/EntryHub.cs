// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
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



        private void SignalPrepared(in Identifier identifier)
        {
            Clients.All.SendAsync("prepared", new object[] { identifier });
            //Clients.All.prepared(identifier)
        }

        private void SignalStored(in Identifier identifier)
        {
            Clients.All.SendAsync("stored", new object[] { identifier });
            //Clients.All.stored(identifier)
        }

    }
}
