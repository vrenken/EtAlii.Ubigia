namespace EtAlii.Servus.Infrastructure.WebApi
{
    using Microsoft.AspNet.SignalR;

    public abstract class HubProxyBase
    {
        protected abstract IHubContext Hub { get; }
    }
}