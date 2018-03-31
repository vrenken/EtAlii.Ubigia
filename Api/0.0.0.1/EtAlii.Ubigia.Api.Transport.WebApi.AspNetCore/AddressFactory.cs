namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    using System;
    using System.Collections.Generic;

	public class AddressFactory : IAddressFactory
	{
		public Uri Create(Uri baseAddress, string relativeAddress, params string[] parameters)
		{
			var queryComponents = new List<string>();
			if (parameters.Length > 1)
			{
				for (int i = 0; i < parameters.Length; i += 2)
				{
					var key = parameters[i];
					var value = Uri.EscapeDataString(parameters[i + 1]);

					queryComponents.Add(String.Join("=", key, value));
				}
			}
			else if (parameters.Length == 1)
			{
				queryComponents.Add(parameters[0]);
			}

			var builder = new UriBuilder(baseAddress)
			{
				Query = String.Join("&", queryComponents)
			};
			builder.Path += "/" + relativeAddress;
			return builder.Uri;//ToString();
		}

		public Uri Create(Storage storage, string relativeAddress, params string[] parameters)
		{
			var baseAddress = new Uri(storage.Address, UriKind.Absolute);
			return Create(baseAddress, relativeAddress, parameters);
		}
	}

}
