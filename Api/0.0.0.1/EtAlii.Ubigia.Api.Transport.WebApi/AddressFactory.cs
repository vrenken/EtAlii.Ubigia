namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Collections.Generic;

    public class AddressFactory : IAddressFactory
    { 
        public Uri Create(Uri baseAddress, string relativeAddress, params string[] parameters)
        {
	        relativeAddress = relativeAddress ?? String.Empty;

			var queryComponents = new List<string>();
	        for (int i = 0; i < parameters.Length; i += 2)
	        {
		        var key = parameters[i];
		        if (i + 1 < parameters.Length)
		        {
			        var value = Uri.EscapeDataString(parameters[i + 1]);
			        queryComponents.Add(String.Join("=", key, value));
		        }
		        else
		        {
			        queryComponents.Add(key);
		        }
	        }

			var builder = new UriBuilder(baseAddress)
	        {
		        Query = String.Join("&", queryComponents)
	        };
	        builder.Path = builder.Path.TrimEnd('/') + "/" + RelativeUri.ApiRest + relativeAddress.TrimStart('/');
	        return builder.Uri;//ToString();
        }

		public Uri Create(Storage storage, string relativeAddress, params string[] parameters)
		{
			var baseAddress = new Uri(storage.Address, UriKind.Absolute);
			return Create(baseAddress, relativeAddress, parameters);
        }
    }
}
