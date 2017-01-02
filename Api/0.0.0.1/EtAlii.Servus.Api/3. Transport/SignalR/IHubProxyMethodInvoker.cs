namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    public interface IHubProxyMethodInvoker
    {
        Task<T> Invoke<T>(IHubProxy proxy, string proxyName, string methodName, params object[] parameters);
        Task Invoke(IHubProxy proxy, string proxyName, string methodName, params object[] parameters);
    }
}