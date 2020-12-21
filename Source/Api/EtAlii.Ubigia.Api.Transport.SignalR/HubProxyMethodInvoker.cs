namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    public class HubProxyMethodInvoker : IHubProxyMethodInvoker
    {
        private const string _errorMessageFormat = "Unable to invoke method '{0}' on hub '{1}'";

        public async IAsyncEnumerable<T> Stream<T>(HubConnection connection, string proxyName, string methodName, params object[] parameters)
            where T: class
        {
            // The structure below might seem weird.
            // But it is not possible to combine a try-catch with the yield needed
            // enumerating an IAsyncEnumerable.
            // The only way to solve this is using the enumerator. 
            var enumerator = connection
                .StreamAsyncCore<T>(methodName, parameters)
                .GetAsyncEnumerator();
            var hasResult = true;
            while (hasResult)
            {
                T item;
                try
                {
                    hasResult = await enumerator
                        .MoveNextAsync()
                        .ConfigureAwait(false);
                    item = hasResult ? enumerator.Current : null;
                }
                catch (Exception e) when (
                    e.Message == "Invalid account" || 
                    e.Message == "Unauthorized" ||
                    e.Message == "Invalid identifier" ||
                    e.Message == "Missing Authentication-Token" ||
                    e.Message.StartsWith("Unauthorized account") ||
                    e.Message.Contains("'Authentication'") ||
                    e.Message.Contains("'Authenticate'") ||
                    e.Message.Contains("'GetLocalStorage'"))
                {
                    throw new UnauthorizedInfrastructureOperationException(e.Message, e);
                }
                catch (AggregateException e)
                {
                    var message = string.Format(_errorMessageFormat, methodName, proxyName);
                    throw new InvalidInfrastructureOperationException(message, e.InnerException);
                }
                catch (Exception e)
                {
                    var message = string.Format(_errorMessageFormat, methodName, proxyName);
                    throw new InvalidInfrastructureOperationException(message, e);
                }
                if (item != null)
                {
                    yield return item;
                }
            }
        }

        public async Task<T> Invoke<T>(HubConnection connection, string proxyName, string methodName, params object[] parameters)
        {
            try
            {
                return await connection.InvokeCoreAsync<T>(methodName, parameters).ConfigureAwait(false);
            }
            catch (Exception e) when (
                e.Message == "Invalid account" || 
                e.Message == "Unauthorized" ||
                e.Message == "Invalid identifier" ||
                e.Message == "Missing Authentication-Token" ||
                e.Message.StartsWith("Unauthorized account") ||
                e.Message.Contains("'Authentication'") ||
                e.Message.Contains("'Authenticate'") ||
                e.Message.Contains("'GetLocalStorage'"))
            {
                throw new UnauthorizedInfrastructureOperationException(e.Message, e);
            }
            catch (AggregateException e)
            {
                var message = string.Format(_errorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e.InnerException);
            }
            catch (Exception e)
            {
                var message = string.Format(_errorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e);
            }
        }

        public async Task Invoke(HubConnection connection, string proxyName, string methodName, params object[] parameters)
        {
            try
            {
                await connection.InvokeCoreAsync(methodName, parameters).ConfigureAwait(false);
            }
            catch (Exception e) when (
                e.Message == "Invalid account" ||
                e.Message == "Unauthorized" ||
                e.Message == "Invalid identifier" ||
                e.Message == "Missing Authentication-Token" ||
                e.Message.StartsWith("Unauthorized account") ||
                e.Message.Contains("'Authentication'") ||
                e.Message.Contains("'Authenticate'") ||
                e.Message.Contains("'GetLocalStorage'"))
            {
                throw new UnauthorizedInfrastructureOperationException(e.Message, e);
            }
            catch (AggregateException e)
            {
                var message = string.Format(_errorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e.InnerException);
            }
            catch (Exception e)
            {
                var message = string.Format(_errorMessageFormat, methodName, proxyName);
                throw new InvalidInfrastructureOperationException(message, e);
            }
        }
    }
}
