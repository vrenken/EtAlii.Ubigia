﻿namespace EtAlii.Servus.Hosting
{
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using System;

    public abstract class HubProxyBase<THub> : HubProxyBase
         where THub : IHub
    {
        protected override IHubContext Hub { get { return _hub.Value; } }
        private readonly Lazy<IHubContext> _hub;

        protected HubProxyBase()
        {
            _hub = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<THub>());
        }
    }
}