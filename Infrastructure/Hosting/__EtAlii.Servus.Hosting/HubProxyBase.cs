namespace EtAlii.Servus.Hosting
{
    using Microsoft.AspNet.SignalR;

    public abstract class HubProxyBase
    {
        protected abstract IHubContext Hub { get; }
    }
}