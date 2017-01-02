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

    public class Infrastructure
    {
        public static string AuthenticationToken { get; set; }

        private const string _mediaType = "application/json";

        //internal Cmdlet Cmdlet { get; set; }

        private readonly string _hostIdentifier;

        public Infrastructure()
        {
            //var bytes = NetworkInterface.GetAllNetworkInterfaces()
            //                            .First()
            //                            .GetPhysicalAddress()
            //                            .GetAddressBytes();
            var bytes = (byte[])Array.CreateInstance(typeof(byte), 64);
            var rnd = new Random();
            rnd.NextBytes(bytes);
            _hostIdentifier = Convert.ToBase64String(bytes);
        }

        public T Get<T>(string address, ICredentials credentials = null)
        {
            Task<HttpResponseMessage> response;
            byte[] bytes = null;

            using (var client = CreateHttpClient(credentials))
            {
                try
                {
                    response = client.GetAsync(address);
                    WaitAndTestResponse(response, HttpMethod.Get);
                    bytes = ParseResponse(response);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException("Unable to GET data from the infrastructure", e);
                }
            }

            var result = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            var o = JsonConvert.DeserializeObject<T>(result);
            return o;
        }

        public T Post<T>(string address, T value = null, ICredentials credentials = null)
            where T : class
        {
            return Post<T, T>(address, value, credentials);
        }

        public U Post<T, U>(string address, T value = null, ICredentials credentials = null)
            where T: class
            where U: class
        {
            Task<HttpResponseMessage> response;
            byte[] bytes = null;

            using (var client = CreateHttpClient(credentials))
            {
                var bytesToPost = GetBytesToSend(value);
                using (var stream = new MemoryStream(bytesToPost))
                {
                    using (var content = new System.Net.Http.StreamContent(stream))
                    {
                        try
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue(_mediaType);
                            response = client.PostAsync(address, content);
                            WaitAndTestResponse(response, HttpMethod.Post);
                            bytes = ParseResponse(response);
                        }
                        catch (AggregateException e)
                        {
                            throw new InfrastructureConnectionException("Unable to POST data to the infrastructure", e);
                        }
                    }
                }
            }

            var result = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            var o = JsonConvert.DeserializeObject<U>(result);
            return o;
        }

        public void Delete(string address, ICredentials credentials = null)
        {
            Task<HttpResponseMessage> response;
            byte[] bytes = null;

            using (var client = CreateHttpClient(credentials))
            {
                try
                {
                    response = client.DeleteAsync(address);
                    WaitAndTestResponse(response, HttpMethod.Delete);
                    bytes = ParseResponse(response);
                }
                catch (AggregateException e)
                {
                    throw new InfrastructureConnectionException("Unable to DELETE data on the infrastructure", e);
                }
            }
        }

        public T Put<T>(string address, T value, ICredentials credentials = null)
        {
            Task<HttpResponseMessage> response;
            byte[] bytes = null;

            using (var client = CreateHttpClient(credentials))
            {
                var bytesToPut = GetBytesToSend(value);
                using (var stream = new MemoryStream(bytesToPut))
                {
                    using (var content = new System.Net.Http.StreamContent(stream))
                    {
                        try
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue(_mediaType );
                            response = client.PutAsync(address, content);
                            WaitAndTestResponse(response, HttpMethod.Put);
                            bytes = ParseResponse(response);
                        }
                        catch (AggregateException e)
                        {
                            throw new InfrastructureConnectionException("Unable to PUT data on the infrastructure", e);
                        }
                    }
                }
            }

            var result = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            var o = JsonConvert.DeserializeObject<T>(result);
            return o;
        }

        private HttpClient CreateHttpClient(ICredentials credentials)
        {
            var client = new HttpClient(new ClientHttpMessageHandler(credentials, _hostIdentifier, AuthenticationToken));
            return client;
        }

        private byte[] GetBytesToSend(object value)
        {
            var bytes = new byte[] { };
            if (value != null)
            {
                var serializedValue = JsonConvert.SerializeObject(value);
                bytes = Encoding.UTF8.GetBytes(serializedValue);
            }
            return bytes;
        }

        private void WaitAndTestResponse(Task<HttpResponseMessage> response, HttpMethod method)
        {
            response.Wait();

            var result = response.Result;
            if (!result.IsSuccessStatusCode)
            {
                Exception e;
                string message;

                switch (result.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        message = String.Format("Unable to {0} data on the infrastructure", method);
                        e = new InvalidInfrastructureOperationException(message);
                        break;
                    case HttpStatusCode.Unauthorized:
                        message = String.Format("Unauthorized to {0} data on the infrastructure", method);
                        e = new UnauthorizedInfrastructureOperationException(message);
                        break;
                    default:
                        message = String.Format("Unable to {0} data on the infrastructure ({1})", method, result.StatusCode);
                        e = new InvalidInfrastructureOperationException(message);
                        break;
                }

                throw e;
            }
        }

        private byte[] ParseResponse(Task<HttpResponseMessage> response)
        {
            var bytesRead = response.Result.Content.ReadAsByteArrayAsync();
            bytesRead.Wait();
            return bytesRead.Result;
        }
    }
}
