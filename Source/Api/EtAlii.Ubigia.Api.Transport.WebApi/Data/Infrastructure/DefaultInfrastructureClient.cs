﻿namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    public sealed class DefaultInfrastructureClient : IInfrastructureClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string _getErrorMessage = "Unable to GET data from the client";
        private const string _postErrorMessage = "Unable to POST data to the client";
        private const string _putErrorMessage = "Unable to PUT data on the client";
        private const string _deleteErrorMessage = "Unable to DELETE data on the client";

        public string AuthenticationToken { get; set; }

        private readonly string _hostIdentifier;
        private readonly PayloadMediaTypeFormatter _formatter;

        public DefaultInfrastructureClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _formatter = new PayloadMediaTypeFormatter();
            var bytes = new byte[64];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            _hostIdentifier = Convert.ToBase64String(bytes);
        }

        public async Task<TResult> Get<TResult>(Uri address, ICredentials credentials = null)
        {
            using var client = _httpClientFactory.Create(credentials, _hostIdentifier, AuthenticationToken);
            
            try
            {
                var response = await client.GetAsync(address).ConfigureAwait(false);
                await WaitAndTestResponse(response, HttpMethod.Get, address).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TResult>(new [] {_formatter }).ConfigureAwait(false);
            }
            catch (UnauthorizedInfrastructureOperationException)
            {
                throw;
            }
            catch (InvalidInfrastructureOperationException)
            {
                throw;
            }
            catch (AggregateException e)
            {
                throw new InfrastructureConnectionException(_getErrorMessage, e.InnerException);
            }
            catch (Exception e)
            {
                throw new InfrastructureConnectionException(_getErrorMessage, e);
            }
        }

        public async Task<TValue> Post<TValue>(Uri address, TValue value = null, ICredentials credentials = null)
            where TValue : class
        {
            return await Post<TValue, TValue>(address, value, credentials).ConfigureAwait(false);
        }

        public async Task<TResult> Post<TValue, TResult>(Uri address, TValue value = null,
            ICredentials credentials = null)
            where TValue : class
            where TResult : class
        {
            using var client = _httpClientFactory.Create(credentials, _hostIdentifier, AuthenticationToken);

            try
            {
                var response = await client.PostAsync(address, value, _formatter).ConfigureAwait(false);
                await WaitAndTestResponse(response, HttpMethod.Post, address).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TResult>(new[] {_formatter}).ConfigureAwait(false);
            }
            catch (UnauthorizedInfrastructureOperationException)
            {
                throw;
            }
            catch (InvalidInfrastructureOperationException)
            {
                throw;
            }
            catch (AggregateException e)
            {
                throw new InfrastructureConnectionException(_postErrorMessage, e.InnerException);
            }
            catch (Exception e)
            {
                throw new InfrastructureConnectionException(_postErrorMessage, e);
            }
        }

	    public async Task Delete(Uri address, ICredentials credentials = null)
        {
            using var client = _httpClientFactory.Create(credentials, _hostIdentifier, AuthenticationToken);
            
            try
            {
                var response = await client.DeleteAsync(address).ConfigureAwait(false);
                await WaitAndTestResponse(response, HttpMethod.Delete, address).ConfigureAwait(false);
            }
            catch (UnauthorizedInfrastructureOperationException)
            {
                throw;
            }
            catch (InvalidInfrastructureOperationException)
            {
                throw;
            }
            catch (AggregateException e)
            {
                throw new InfrastructureConnectionException(_deleteErrorMessage, e.InnerException);
            }
            catch (Exception e)
            {
                throw new InfrastructureConnectionException(_deleteErrorMessage, e);
            }
        }

        public async Task<TValue> Put<TValue>(Uri address, TValue value, ICredentials credentials = null)
        {
            using var client = _httpClientFactory.Create(credentials, _hostIdentifier, AuthenticationToken);
            
            try
            {
                var response = await client.PutAsync(address, value, _formatter).ConfigureAwait(false);
                await WaitAndTestResponse(response, HttpMethod.Put, address).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TValue>(new[] { _formatter }).ConfigureAwait(false);
            }
            catch (UnauthorizedInfrastructureOperationException)
            {
                throw;
            }
            catch (InvalidInfrastructureOperationException)
            {
                throw;
            }
            catch (AggregateException e)
            {
                throw new InfrastructureConnectionException(_putErrorMessage, e.InnerException);
            }
            catch (Exception e)
            {
                throw new InfrastructureConnectionException(_putErrorMessage, e);
            }
        }


        private async Task WaitAndTestResponse(HttpResponseMessage result, HttpMethod method, Uri address)
        {
    
            if (!result.IsSuccessStatusCode)
            {
                Exception e;
                string message;

                var error = await GetError(result).ConfigureAwait(false);

                switch (result.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        message = $"Unable to {method} {address}: {error}";
                        e = new InvalidInfrastructureOperationException(message);
                        break;
                    case HttpStatusCode.Unauthorized:
                        message = $"Unauthorized to {method} {address}: {error}";
                        e = new UnauthorizedInfrastructureOperationException(message);
                        break;
                    default:
                        message = string.Format("Unable to {0} {3} ({2}): {1}", method, error, result.StatusCode, address);
                        e = new InvalidInfrastructureOperationException(message);
                        break;
                }
                throw e;
            }
        }

        private async Task<string> GetError(HttpResponseMessage response)
        {
            var errorString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            errorString = string.IsNullOrWhiteSpace(errorString) ? "UNKNOWN" : errorString;
            return errorString;
        }
	}
}
