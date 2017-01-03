﻿namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;

    public sealed class DefaultInfrastructureClient : IInfrastructureClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string GetErrorMessage = "Unable to GET data from the client";
        private const string PostErrorMessage = "Unable to POST data to the client";
        private const string PutErrorMessage = "Unable to PUT data on the client";
        private const string DeleteErrorMessage = "Unable to DELETE data on the client";

        public string AuthenticationToken
        {
            get { return _authenticationToken; }
            set { _authenticationToken = value; }
        }

        private string _authenticationToken;
        private readonly string _hostIdentifier;
        private readonly PayloadMediaTypeFormatter _formatter;

        public DefaultInfrastructureClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _formatter = new PayloadMediaTypeFormatter();
            var bytes = new byte[64];
            var rnd = new Random();
            rnd.NextBytes(bytes);
            _hostIdentifier = Convert.ToBase64String(bytes);
        }

        public async Task<TResult> Get<TResult>(string address, ICredentials credentials = null)
        {
            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                try
                {
                    var response = await client.GetAsync(address);
                    await WaitAndTestResponse(response, HttpMethod.Get);
                    return await ParseResult<TResult>(response);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException(GetErrorMessage, e);
                }
            }
        }

        public async Task<TValue> Post<TValue>(string address, TValue value = null, ICredentials credentials = null)
            where TValue : class
        {
            return await Post<TValue, TValue>(address, value, credentials);
        }

        public async Task<TResult> Post<TValue, TResult>(string address, TValue value = null,
            ICredentials credentials = null)
            where TValue : class
            where TResult : class
        {
            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                try
                {
                    var response = await client.PostAsync(address, value, _formatter);
                    await WaitAndTestResponse(response, HttpMethod.Post);
                    return await ParseResult<TResult>(response);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException(PostErrorMessage, e);
                }
            }
        }

        public async Task Delete(string address, ICredentials credentials = null)
        {
            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                try
                {
                    var response = await client.DeleteAsync(address);
                    await WaitAndTestResponse(response, HttpMethod.Delete);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException(DeleteErrorMessage, e);
                }
            }
        }

        public async Task<TValue> Put<TValue>(string address, TValue value, ICredentials credentials = null)
        {
            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                try
                {
                    var response = await client.PutAsync(address, value, _formatter);
                    await WaitAndTestResponse(response, HttpMethod.Put);
                    return await ParseResult<TValue>(response);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException(PutErrorMessage, e);
                }
            }
        }


        private async Task WaitAndTestResponse(HttpResponseMessage result, HttpMethod method)
        {
    
            if (!result.IsSuccessStatusCode)
            {
                Exception e = null;
                string message;

                var error = await GetError(result);

                switch (result.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        message = String.Format("Unable to {0} data on the client: {1}", method, error);
                        e = new InvalidInfrastructureOperationException(message);
                        break;
                    case HttpStatusCode.Unauthorized:
                        message = String.Format("Unauthorized to {0} data on the client: {1}", method, error);
                        e = new UnauthorizedInfrastructureOperationException(message);
                        break;
                    default:
                        message = String.Format("Unable to {0} data on the client ({2}): {1}", method, error, result.StatusCode);
                        e = new InvalidInfrastructureOperationException(message);
                        break;
                }
                throw e;
            }
        }

        private async Task<string> GetError(HttpResponseMessage response)
        {
            var errorString = await response.Content.ReadAsStringAsync();
            errorString = String.IsNullOrWhiteSpace(errorString) ? "UNKNOWN" : errorString;
            return errorString;
        }

        private async Task<TValue> ParseResult<TValue>(HttpResponseMessage result)
        {
            return await result.Content.ReadAsAsync<TValue>(new[] {_formatter});
        }
    }
}
