namespace EtAlii.Ubigia.Api.Transport.Rest
{
	using System;
	using System.Collections.Generic;

	public class AddressFactory : IAddressFactory
	{
		public Uri Create(Uri baseAddress, string relativeAddress, params string[] parameters)
		{
			relativeAddress ??= string.Empty;

			var queryComponents = new List<string>();
			for (var i = 0; i < parameters.Length; i += 2)
			{
				var key = parameters[i];
				if (i + 1 < parameters.Length)
				{
					var value = Uri.EscapeDataString(parameters[i + 1]);
					queryComponents.Add(string.Join("=", key, value));
				}
				else
				{
					queryComponents.Add(key);
				}
			}

			var builder = new UriBuilder(baseAddress)
			{
				Query = string.Join("&", queryComponents)
			};

			builder.Path = builder.Path.TrimEnd('/') + UriHelper.Delimiter + relativeAddress.TrimStart('/');
			return builder.Uri;//ToString()
		}

		public Uri Create(ISpaceTransport transport, string relativeAddress, params string[] parameters)
		{
			return Create(transport.Address, relativeAddress, parameters);
		}
		public Uri Create(IStorageTransport transport, string relativeAddress, params string[] parameters)
		{
			return Create(transport.Address, relativeAddress, parameters);
		}
		public Uri Create(Storage storage, string relativeAddress, params string[] parameters)
		{
			var baseAddress = new Uri(storage.Address, UriKind.Absolute);
			return Create(baseAddress, relativeAddress, parameters);
		}
	}

}
