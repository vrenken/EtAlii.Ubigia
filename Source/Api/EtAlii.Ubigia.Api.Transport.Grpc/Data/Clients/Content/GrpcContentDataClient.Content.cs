// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;
    using Content = EtAlii.Ubigia.Content;
    using ContentPart = EtAlii.Ubigia.ContentPart;
    using Identifier = EtAlii.Ubigia.Identifier;

    internal partial class GrpcContentDataClient
    {
        public async Task Store(Identifier identifier, Content content)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new ContentPostRequest {EntryId = identifier.ToWire(), Content = content.ToWire()};
                await _contentClient.PostAsync(request, metadata);

                // Should this call be replaced by get instead?
                // More details can be found in the Github issue below:
                // https://github.com/vrenken/EtAlii.Ubigia/issues/80
                Blob.SetStored(content, true);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task Store(Identifier identifier, ContentPart contentPart)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new ContentPartPostRequest {EntryId = identifier.ToWire(), ContentPart = contentPart.ToWire(), ContentPartId = contentPart.Id };
                await _contentClient.PostPartAsync(request, metadata);
                BlobPart.SetStored(contentPart, true);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<Content> Retrieve(Identifier identifier)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new ContentGetRequest { EntryId = identifier.ToWire() };
                var response = await _contentClient.GetAsync(request, metadata);
                return response.Content.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new ContentPartGetRequest { EntryId = identifier.ToWire(), ContentPartId = contentPartId};
                var response = await _contentClient.GetPartAsync(request, metadata);
                return response.ContentPart.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }
    }
}
