// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    public class ContentDataClientStub : IContentDataClient
    {
        public Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
        {
            return Task.CompletedTask;
        }

        public Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            return Task.CompletedTask;
        }

        public Task<ContentDefinition> RetrieveDefinition(Identifier identifier)
        {
            return Task.FromResult<ContentDefinition>(null);
        }

        public Task Store(Identifier identifier, Content content)
        {
            return Task.CompletedTask;
        }

        public Task Store(Identifier identifier, ContentPart contentPart)
        {
            return Task.CompletedTask;
        }

        public Task<Content> Retrieve(Identifier identifier)
        {
            return Task.FromResult<Content>(null);
        }

        public Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId)
        {
            return Task.FromResult<ContentPart>(null);
        }

        public Task Connect(ISpaceConnection spaceConnection)
        {
            return Task.CompletedTask;
        }

        public Task Disconnect()
        {
            return Task.CompletedTask;
        }
    }
}
