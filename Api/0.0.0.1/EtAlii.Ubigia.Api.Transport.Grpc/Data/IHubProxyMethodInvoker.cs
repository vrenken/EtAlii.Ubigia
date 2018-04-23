namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Grpc.Client;

    public interface IHubProxyMethodInvoker
    {
        Task<T> Invoke<T>(HubConnection connection, string proxyName, string methodName, params object[] parameters);
        Task Invoke(HubConnection connection, string proxyName, string methodName, params object[] parameters);
    }
}