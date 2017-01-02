namespace EtAlii.Servus.Api
{
    using System;
    using System.Net;
    using System.Reflection;
    using EtAlii.xTechnology.Logging;

    public class LoggingInfrastructureClient : IInfrastructureClient
    {
        private readonly IInfrastructureClient _client;
        private readonly ILogger _logger;

        public LoggingInfrastructureClient(
            IInfrastructureClient client,
            ILogger logger)
        {
            _client = client;
            _logger = logger;
        }


        public string AuthenticationToken { get { return _client.AuthenticationToken; } set { _client.AuthenticationToken = value; } }

        public T Get<T>(string address, ICredentials credentials = null)
        {
            var message = String.Format("Invoking GET on infrastructure (Url: {0} Type: {1})", address, typeof(T).Name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var result = _client.Get<T>(address, credentials);

            message = String.Format("Invoked GET on infrastructure (Url: {0} Type: {1} Duration: {2}ms)", address, typeof(T).Name, Environment.TickCount - start);
            _logger.Info(message);

            return result;
        }

        public T Post<T>(string address, T value = default(T), ICredentials credentials = null) 
            where T : class
        {
            var message = String.Format("Invoking POST on infrastructure (Url: {0} Type: {1})", address, typeof(T).Name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var result = _client.Post<T>(address, value, credentials);

            message = String.Format("Invoked POST on infrastructure (Url: {0} Type: {1} Duration: {2}ms)", address, typeof(T).Name, Environment.TickCount - start);
            _logger.Info(message);

            return result;

        }

        public U Post<T, U>(string address, T value = default(T), ICredentials credentials = null) 
            where T : class 
            where U : class
        {
            var message = String.Format("Invoking POST on infrastructure (Url: {0} Types: {1}, {2})", address, typeof(T).Name, typeof(U).Name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var result = _client.Post<T, U>(address, value, credentials);

            message = String.Format("Invoked POST on infrastructure (Url: {0} Types: {1}, {2} Duration: {3}ms)", address, typeof(T).Name, typeof(U).Name, Environment.TickCount - start);
            _logger.Info(message);

            return result;
        }

        public void Delete(string address, ICredentials credentials = null)
        {
            var message = String.Format("Invoking DELETE on infrastructure (Url: {0})", address);
            _logger.Info(message);
            var start = Environment.TickCount;

            _client.Delete(address, credentials);

            message = String.Format("Invoked DELETE on infrastructure (Url: {0} Duration: {1}ms)", address, Environment.TickCount - start);
            _logger.Info(message);
        }

        public T Put<T>(string address, T value, ICredentials credentials = null)
        {
            var message = String.Format("Invoking PUT on infrastructure (Url: {0} Type: {1})", address, typeof(T).Name);
            _logger.Info(message);
            var start = Environment.TickCount;

            var result = _client.Put<T>(address, value, credentials);

            message = String.Format("Invoked PUT on infrastructure (Url: {0} Type: {1} Duration: {2}ms)", address, typeof(T).Name, Environment.TickCount - start);
            _logger.Info(message);

            return result;
        }
    }
}
