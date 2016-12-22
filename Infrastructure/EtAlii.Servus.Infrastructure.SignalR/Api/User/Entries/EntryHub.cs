namespace EtAlii.Servus.Infrastructure.SignalR
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure.Functional;

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
            Clients.All.prepared(identifier);

            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<EntryNotificationHub>();
            //hubContext.Clients.All.prepared(identifier);

            //Hub.Clients.Group(identifier.Space.ToString()).Prepared(identifier);
        }

        private void SignalStored(Identifier identifier)
        {
            Clients.All.stored(identifier);
            //Hub.Clients.Group(identifier.Space.ToString()).Stored(identifier);
        }

    }
}
