namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AddressFactory
    {
        public string Create(Storage storage, string path, params string[] parameters)
        {
            var queryComponents = new List<string>();
            for (int i = 0; i < parameters.Length; i += 2)
            {
                var key = parameters[i];
                var value = Uri.EscapeDataString(parameters[i + 1]);

                queryComponents.Add(String.Join("=", key, value));
            }

            var builder = new UriBuilder(storage.Address);
            builder.Path = path;
            builder.Query = String.Join("&", queryComponents);
            return builder.ToString();
        }
    }
}
