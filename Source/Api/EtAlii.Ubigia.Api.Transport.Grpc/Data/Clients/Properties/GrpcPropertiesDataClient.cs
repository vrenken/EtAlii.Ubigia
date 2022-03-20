// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Serialization;
    using global::Grpc.Core;
    using Identifier = EtAlii.Ubigia.Identifier;
    using PropertyDictionary = EtAlii.Ubigia.PropertyDictionary;

    internal class GrpcPropertiesDataClient : GrpcClientBase, IPropertiesDataClient<IGrpcSpaceTransport>
    {
        private IGrpcSpaceTransport _transport;
        private PropertiesGrpcService.PropertiesGrpcServiceClient _client;
        private readonly ISerializer _serializer;

        public GrpcPropertiesDataClient(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new PropertiesPostRequest{EntryId = identifier.ToWire(), PropertyDictionary = properties.ToWire(_serializer)};
                await _client.PostAsync(request, metadata);
                PropertiesHelper.SetStored(properties, true);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcPropertiesDataClient)}.Store()", e);
            }
        }

        public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new PropertiesGetRequest{ EntryId = identifier.ToWire() };
                var response = await _client.GetAsync(request, metadata);
                var result = response.PropertyDictionary.ToLocal(_serializer);

                if (result != null)
                {
                    PropertiesHelper.SetStored(result, true);
                    // properties.Stored is not serialized in the PropertyDictionaryConverter.
                }
                return result;
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcPropertiesDataClient)}.Retrieve()", e);
            }
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection).ConfigureAwait(false);

            _transport = ((IGrpcSpaceConnection)spaceConnection).Transport;
            _client = new PropertiesGrpcService.PropertiesGrpcServiceClient(_transport.CallInvoker);
        }

        public override async Task Disconnect()
        {
            await base.Disconnect().ConfigureAwait(false);
            _transport = null;
            _client = null;
        }
    }
}
