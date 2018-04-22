namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    public interface IHubProxyMethodInvoker
    {
        Task<T> Invoke<T>(HubConnection connection, string proxyName, string methodName, params object[] parameters);
        Task Invoke(HubConnection connection, string proxyName, string methodName, params object[] parameters);
    }
}