//namespace EtAlii.Ubigia.Api.Transport.Grpc
//[
//    using System
//    using System.Threading.Tasks

//    public class HubProxyMethodInvoker : IHubProxyMethodInvoker
//    [
//        private const string ErrorMessageFormat = "Unable to invoke method '{0}' on hub '{1}'"

//        public async Task<T> Invoke<T>(HubConnection connection, string proxyName, string methodName, params object[] parameters)
//        [
//            try
//            [
//                return await connection.InvokeAsync<T>(methodName, parameters)
//            ]
//            catch (Exception e) when (
//                e.Message == "Invalid account" || 
//                e.Message == "Unauthorized" ||
//                e.Message == "Invalid identifier" ||
//                e.Message == "Missing Authentication-Token" ||
//                e.Message.StartsWith("Unauthorized account")) 
//            [
//                throw new UnauthorizedInfrastructureOperationException(e.Message, e)
//            ]
//            catch (AggregateException e)
//            [
//                var message = String.Format(ErrorMessageFormat, methodName, proxyName)
//                throw new InvalidInfrastructureOperationException(message, e.InnerException)
//            ]
//            catch (Exception e)
//            [
//                var message = String.Format(ErrorMessageFormat, methodName, proxyName)
//                throw new InvalidInfrastructureOperationException(message, e)
//            ]
//        ]
//        public async Task Invoke(HubConnection connection, string proxyName, string methodName, params object[] parameters)
//        [
//            try
//            [
//                await connection.InvokeAsync(methodName, parameters)
//            ]
//            catch (Exception e) when (
//                e.Message == "Invalid account" ||
//                e.Message == "Unauthorized" ||
//                e.Message == "Invalid identifier" ||
//                e.Message == "Missing Authentication-Token" ||
//                e.Message.StartsWith("Unauthorized account"))
//            [
//                throw new UnauthorizedInfrastructureOperationException(e.Message, e)
//            ]
//            catch (AggregateException e)
//            [
//                var message = String.Format(ErrorMessageFormat, methodName, proxyName)
//                throw new InvalidInfrastructureOperationException(message, e.InnerException)
//            ]
//            catch (Exception e)
//            [
//                var message = String.Format(ErrorMessageFormat, methodName, proxyName)
//                throw new InvalidInfrastructureOperationException(message, e)
//            ]
//        ]
//    ]
//]