//namespace EtAlii.Servus.Infrastructure
//{
//    using System;
//    using System.Net;
//    using System.Threading.Tasks;
//    using EtAlii.Servus.Api.Transport;

//    internal class SystemInfrastructureClient : IInfrastructureClient
//    {
//        private readonly ISystemConnectionConfiguration _configuration;

//        private readonly string _errorMesssage = "All connections that relate to a SystemConnection should not use the InfrastructureClient";

//        public string AuthenticationToken { get { return _authenticationToken; } set { _authenticationToken = value; } }
//        private string _authenticationToken;

//        public SystemInfrastructureClient(ISystemConnectionConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public Task<TResult> Get<TResult>(string address, ICredentials credentials = null)
//        {
//            if (typeof(TResult) == typeof(EtAlii.Servus.Api.Storage))
//            {
//                var result = _configuration.Infrastructure.Storages.GetLocal() as object;
//                return Task.FromResult<TResult>((TResult)result);
//            }
//            if (typeof(TResult) == typeof(string))
//            {
//                var result = "System_" + Guid.NewGuid().ToString().Replace("-","") as object;
//                return Task.FromResult<TResult>((TResult)result);
//            }
//            else
//            {
//                throw new InvalidOperationException(_errorMesssage);
//            }
//        }

//        public Task<TValue> Post<TValue>(string address, TValue value = default(TValue), ICredentials credentials = null) where TValue : class
//        {
//            throw new InvalidOperationException(_errorMesssage);
//        }

//        public Task<TResult> Post<TValue, TResult>(string address, TValue value = default(TValue), ICredentials credentials = null) where TValue : class where TResult : class
//        {
//            throw new InvalidOperationException(_errorMesssage);
//        }

//        public Task Delete(string address, ICredentials credentials = null)
//        {
//            throw new InvalidOperationException(_errorMesssage);
//        }

//        public Task<TValue> Put<TValue>(string address, TValue value, ICredentials credentials = null)
//        {
//            throw new InvalidOperationException(_errorMesssage);
//        }
//    }
//}
