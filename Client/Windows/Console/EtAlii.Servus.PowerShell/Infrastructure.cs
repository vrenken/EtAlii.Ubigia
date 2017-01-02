namespace EtAlii.Servus.PowerShell
{
    using EtAlii.Servus.Client.Model;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Management.Automation;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Text;

    public class Infrastructure
    {
        public static string AuthenticationToken { get; set; }

        internal Cmdlet Cmdlet { get; set; }

        private readonly string _hostIdentifier;

        public Infrastructure()
        {
            var bytes = NetworkInterface.GetAllNetworkInterfaces()
                                        .First()
                                        .GetPhysicalAddress()
                                        .GetAddressBytes();
            _hostIdentifier = Convert.ToBase64String(bytes);
        }

        public T Get<T>(string address)
        {
            return Communicate<T>(address, null, HttpMethod.Get);
        }

        public T Get<T>(string address, ICredentials credentials = null)
        {
            return Communicate<T>(address, null, HttpMethod.Get, credentials);
        }

        public T Get<T>(string address, object value)
        {
            return Communicate<T>(address, value, HttpMethod.Get);
        }

        public T Post<T>(string address, object value)
        {
            return Communicate<T>(address, value, HttpMethod.Post);
        }

        public void Post(string address, object value)
        {
            Communicate(address, value, HttpMethod.Post);
        }

        public void Delete(string address)
        {
            Communicate(address, null, HttpMethod.Delete);
        }

        public T Delete<T>(string address, object value)
        {
            return Communicate<T>(address, value, HttpMethod.Delete);
        }

        public void Delete(string address, object value)
        {
            Communicate(address, value, HttpMethod.Delete);
        }

        public T Put<T>(string address, object value)
        {
            return Communicate<T>(address, value, HttpMethod.Put);
        }

        public void Put<T>(string address, T value)
        {
            Communicate<T>(address, value, HttpMethod.Put);
        }

        private void Communicate(string address, object value, string httpMethod)
        {
            Communicate<object>(address, value, httpMethod);
        }

        private T Communicate<T>(string address, object value, string httpMethod, ICredentials credentials = null)
        {
            using (var client = CreateWebClient(httpMethod, credentials))
            {
                byte[] bytes;
                if (httpMethod == HttpMethod.Get)
                {
                    bytes = client.DownloadData(address);
                }
                else
                {
                    bytes = GetBytesToSend(value);
                    bytes = client.UploadData(address, httpMethod, bytes);
                }
                var result = Encoding.UTF8.GetString(bytes);
                var o = JsonConvert.DeserializeObject<T>(result);
                return o;
            }
        }

        private WebClient CreateWebClient(string httpMethod, ICredentials credentials)
        {
            var client = new WebClient();
            client.Credentials = credentials;
            client.Headers["Host-Identifier"] = _hostIdentifier;
            client.Headers["Authentication-Token"] = AuthenticationToken;
            client.Headers.Add("Content-Type", "application/json");
            client.Encoding = Encoding.UTF8;
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
    }
}
