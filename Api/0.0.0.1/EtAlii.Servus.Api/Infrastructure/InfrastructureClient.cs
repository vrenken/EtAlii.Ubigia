namespace EtAlii.Servus.Api
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class InfrastructureClient : IInfrastructureClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string MediaType = "application/json";
        private const string GetErrorMessage = "Unable to GET data from the client";
        private const string PostErrorMessage = "Unable to POST data to the client";
        private const string PutErrorMessage = "Unable to PUT data on the client";
        private const string DeleteErrorMessage = "Unable to DELETE data on the client";

        public string AuthenticationToken { get { return _authenticationToken; } set { _authenticationToken = value; } }
        private string _authenticationToken;
        private readonly IPayloadSerializer _payloadSerializer;
        private readonly string _hostIdentifier;

        public InfrastructureClient(IPayloadSerializer payloadSerializer, IHttpClientFactory httpClientFactory)
        {
            _payloadSerializer = payloadSerializer;
            _httpClientFactory = httpClientFactory;

            //var bytes = NetworkInterface.GetAllNetworkInterfaces()
            //                            .First()
            //                            .GetPhysicalAddress()
            //                            .GetAddressBytes();
            var bytes = new byte[64];
            var rnd = new Random();
            rnd.NextBytes(bytes);
            _hostIdentifier = Convert.ToBase64String(bytes);
        }

        public TResult Get<TResult>(string address, ICredentials credentials = null)
        {
            byte[] bytes;

            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                try
                {
                    Task<HttpResponseMessage> response = client.GetAsync(address);
                    WaitAndTestResponse(response, System.Net.Http.HttpMethod.Get);
                    bytes = ParseResponse(response);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException(GetErrorMessage, e);
                }
            }

            var result = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            var o = JsonConvert.DeserializeObject<TResult>(result);
            return o;
        }

        public TValue Post<TValue>(string address, TValue value = null, ICredentials credentials = null)
            where TValue : class
        {
            return Post<TValue, TValue>(address, value, credentials);
        }

        public TResult Post<TValue, TResult>(string address, TValue value = null, ICredentials credentials = null)
            where TValue : class
            where TResult : class
        {
            byte[] bytes;

            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                var bytesToPost = _payloadSerializer.Serialize(value);
                using (var stream = new MemoryStream(bytesToPost))
                {
                    using (var content = new System.Net.Http.StreamContent(stream))
                    {
                        try
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
                            Task<HttpResponseMessage> response = client.PostAsync(address, content);
                            WaitAndTestResponse(response, System.Net.Http.HttpMethod.Post);
                            bytes = ParseResponse(response);
                        }
                        catch (AggregateException e)
                        {
                            throw new InfrastructureConnectionException(PostErrorMessage, e);
                        }
                    }
                }
            }

            var o = _payloadSerializer.Deserialize<TResult>(bytes);
            return o;
        }

        public void Delete(string address, ICredentials credentials = null)
        {
            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                try
                {
                    Task<HttpResponseMessage> response = client.DeleteAsync(address);
                    WaitAndTestResponse(response, System.Net.Http.HttpMethod.Delete);
                    ParseResponse(response);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException(DeleteErrorMessage, e);
                }
            }
        }

        public TValue Put<TValue>(string address, TValue value, ICredentials credentials = null)
        {
            byte[] bytes;

            using (var client = _httpClientFactory.Create(credentials, _hostIdentifier, _authenticationToken))
            {
                var bytesToPut = _payloadSerializer.Serialize(value);
                using (var stream = new MemoryStream(bytesToPut))
                {
                    using (var content = new System.Net.Http.StreamContent(stream))
                    {
                        try
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
                            Task<HttpResponseMessage> response = client.PutAsync(address, content);
                            WaitAndTestResponse(response, System.Net.Http.HttpMethod.Put);
                            bytes = ParseResponse(response);
                        }
                        catch (AggregateException e)
                        {
                            throw new InfrastructureConnectionException(PutErrorMessage, e);
                        }
                    }
                }
            }

            var result = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            var o = JsonConvert.DeserializeObject<TValue>(result);
            return o;
        }


        private void WaitAndTestResponse(Task<HttpResponseMessage> response, System.Net.Http.HttpMethod method)
        {
            response.Wait();

            var result = response.Result;
            if (!result.IsSuccessStatusCode)
            {
                Exception e;
                string message;

                var error = GetError(response);

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

                //e.Data.Add("Response", result);
                
                throw e;
            }
        }

        private string GetError(Task<HttpResponseMessage> response)
        {
            var error = response.Result.Content.ReadAsStringAsync();
            error.Wait();
            var errorString = error.Result;
            errorString = String.IsNullOrWhiteSpace(errorString) ? "UNKNOWN" : errorString;
            return errorString;
        }

        private byte[] ParseResponse(Task<HttpResponseMessage> response)
        {
            var bytesRead = response.Result.Content.ReadAsByteArrayAsync();
            bytesRead.Wait();
            return bytesRead.Result;
        }
    }
}
