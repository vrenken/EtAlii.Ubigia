// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;
    using ContentDefinition = EtAlii.Ubigia.ContentDefinition;
    using ContentDefinitionPart = EtAlii.Ubigia.ContentDefinitionPart;
    using Identifier = EtAlii.Ubigia.Identifier;

    internal partial class GrpcContentDataClient
    {
        public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new ContentDefinitionPostRequest {EntryId = identifier.ToWire(), ContentDefinition = contentDefinition.ToWire()};
                await _contentDefinitionClient.PostAsync(request, metadata).ConfigureAwait(false);
                MarkAsStored(contentDefinition);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new ContentDefinitionPartPostRequest {EntryId = identifier.ToWire(), ContentDefinitionPart = contentDefinitionPart.ToWire(), ContentDefinitionPartId = contentDefinitionPart.Id };
                await _contentDefinitionClient.PostPartAsync(request, metadata).ConfigureAwait(false);
                MarkAsStored(contentDefinitionPart);
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.StoreDefinition()", e);
            }
        }

        public async Task<ContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            try
            {
                var metadata = new Metadata { _transport.AuthenticationHeader };
                var request = new ContentDefinitionGetRequest { EntryId = identifier.ToWire() };
                var response = await _contentDefinitionClient.GetAsync(request, metadata).ConfigureAwait(false);
                return response.ContentDefinition.ToLocal();
            }
            catch (RpcException e)
            {
                throw new InvalidInfrastructureOperationException($"{nameof(GrpcContentDataClient)}.RetrieveDefinition()", e);
            }
        }

        private void MarkAsStored(ContentDefinition contentDefinition)
        {
            Blob.SetStored(contentDefinition, true);

            foreach (var contentDefinitionPart in contentDefinition.Parts)
            {
                MarkAsStored(contentDefinitionPart);
            }
        }

        private void MarkAsStored(ContentDefinitionPart contentDefinitionPart)
        {
            BlobPart.SetStored(contentDefinitionPart, true);
        }
    }
}
