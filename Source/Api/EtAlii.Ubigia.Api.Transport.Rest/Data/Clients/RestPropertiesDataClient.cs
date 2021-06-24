// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    using System.Threading.Tasks;

    internal class RestPropertiesDataClient : RestClientBase, IPropertiesDataClient<IRestSpaceTransport>
    {
        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Properties, UriParameter.EntryId, identifier.ToString());
            await Connection.Client.Post(address, properties).ConfigureAwait(false);
            PropertiesHelper.SetStored(properties, true);
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            return await scope.Cache.GetProperties(identifier, async () =>
            {
                var address = Connection.AddressFactory.Create(Connection.Transport, RelativeDataUri.Properties, UriParameter.EntryId, identifier.ToString());
                var result = await Connection.Client.Get<PropertyDictionary>(address).ConfigureAwait(false);
                if (result != null)
                {
                    PropertiesHelper.SetStored(result, true);
                    // properties.Stored is not serialized in the PropertyDictionaryConverter.
                }
                return result;
            }).ConfigureAwait(false);
        }
    }
}
