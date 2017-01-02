namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal class HubProxyMethodInvoker : IHubProxyMethodInvoker
    {
        private const string ErrorMessageFormat = "Unable to invoke method '{0}' on hub '{1}'";

        public async Task<T> Invoke<T>(IHubProxy proxy, string proxyName, string methodName, params object[] parameters)
        {
            try
            {
                return await proxy.Invoke<T>(methodName, parameters);
            }
            catch (AggregateException e)
            {
                var message = String.Format(ErrorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e.InnerException);
            }
            catch (Exception e)
            {
                var message = String.Format(ErrorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e);
            }
        }

        public async Task Invoke(IHubProxy proxy, string proxyName, string methodName, params object[] parameters)
        {
            try
            {
                await proxy.Invoke(methodName, parameters);
            }
            catch (AggregateException e)
            {
                var message = String.Format(ErrorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e.InnerException);
            }
            catch (Exception e)
            {
                var message = String.Format(ErrorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e);
            }
        }
    }
}
