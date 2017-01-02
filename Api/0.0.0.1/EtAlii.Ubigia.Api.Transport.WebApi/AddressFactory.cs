namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Collections.Generic;

    public class AddressFactory : IAddressFactory
    {
        public string CreateFullAddress(string address, params string[] fragments)
        {
            var builder = new UriBuilder(address)
            {
                Path = String.Join("/", fragments)
            };
            var result = builder.ToString();
            return result;
        }

        public string Create(Storage storage, string path, params string[] parameters)
        {
            var queryComponents = new List<string>();
            for (int i = 0; i < parameters.Length; i += 2)
            {
                var key = parameters[i];
                var value = Uri.EscapeDataString(parameters[i + 1]);

                queryComponents.Add(String.Join("=", key, value));
            }

            var builder = new UriBuilder(storage.Address)
            {
                Path = path, 
                Query = String.Join("&", queryComponents)
            };
            return builder.ToString();
        }
    }
}
